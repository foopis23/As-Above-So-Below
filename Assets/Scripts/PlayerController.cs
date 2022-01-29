using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Editor Fields
    public float WalkSpeed;
    public float RunSpeed;
    public float WalkAccel;
    public float RunAccel;
    public float AirAccel;
    public float JumpAccel;
    public float LookSensitivity;

    public string FMODEventJump;
    public string FMODEventLand;
    public string FMODEventStep;

    // Private Fields
    private CharacterController characterController;
    private Camera camera;
    private Vector3 velocity;
    private Vector3 movement;
    private Vector2 rotation;
    private bool isRunning;
    private bool isGrounded;
    private bool wasGrounded;

    private FMODHelper fmodHelper;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;

        wasGrounded = true;

        fmodHelper = new FMODHelper(new string[] { FMODEventJump, FMODEventLand, FMODEventStep });

        Debug.Log(fmodHelper);
    }

    // Update is called once per frame
    void Update()
    {
        // camera movement
        rotation.x += Input.GetAxis("Mouse X") * LookSensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * LookSensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        transform.localRotation = xQuat;
		camera.transform.localRotation = yQuat;

        // get velocity
        velocity = characterController.velocity;
        Vector3 velocityLocal2D = transform.InverseTransformDirection(velocity);
        velocityLocal2D.y = 0f;
        isGrounded = characterController.isGrounded;

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;

            if(!wasGrounded)
            {
                fmodHelper.PlayOneshot(FMODEventLand);
            }
        }

        wasGrounded = isGrounded;

        // movement
        isRunning = Input.GetButton("Run");
        float maxSpeed = isRunning || !isGrounded ? RunSpeed : WalkSpeed;
        float maxAccel = (isGrounded ? (isRunning ? RunAccel : WalkAccel) : AirAccel);
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * maxSpeed;
        Vector3 acceleration = Vector3.ClampMagnitude(movement - Vector3.ClampMagnitude(velocityLocal2D, RunSpeed), 1) * maxAccel * Time.deltaTime;
        velocity += transform.TransformDirection(acceleration);

        // gravity
        velocity -= Vector3.Scale(GravitySystem.GravityScale, Physics.gravity) * Time.deltaTime;

        // jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += JumpAccel;

            fmodHelper.PlayOneshot(FMODEventJump);
        }

        characterController.Move(velocity * Time.deltaTime);
        Debug.Log(velocity.magnitude);

        // sound

    }
}
