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
    public float StepsPerSecond;
    public float RunStepsMultiplier;
    public float VerticalRotationSpeed;

    public Transform GroundCheck;

    public string FMODEventJump;
    public string FMODEventLand;
    public string FMODEventStep;

    // Public Properties
    public bool IsHoldingObject { get; set; }

    // Private Fields
    private CharacterController characterController;
    private Camera camera;
    private Vector3 velocity;
    private Vector3 movement;
    private float yRotation;
    private bool isRunning;
    private bool isGrounded;
    private bool wasGrounded;
    private bool needJump;
    private float lastStepTime;

    private FMODHelper fmodHelper;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;

        IsHoldingObject = false;
        wasGrounded = true;
        lastStepTime = Time.time;

        fmodHelper = new FMODHelper(new string[] { FMODEventJump, FMODEventLand, FMODEventStep });
    }

    void Update()
    {
        // camera movement + player rotation
        yRotation += Input.GetAxis("Mouse Y") * LookSensitivity;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);
        
        transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * LookSensitivity, 0f);
        camera.transform.localRotation = Quaternion.AngleAxis(yRotation, Vector3.left);

        float angleFromGravity = Vector3.Angle(transform.up, -GravitySystem.GravityScale);
        Debug.Log(angleFromGravity);
        if(angleFromGravity > 0.0001f)
        {
            Vector3 rotationAxis = Vector3.Cross(transform.up, -GravitySystem.GravityScale).normalized;
            transform.rotation *= Quaternion.AngleAxis(Mathf.Min(VerticalRotationSpeed * Time.deltaTime, angleFromGravity), rotationAxis);
        }

        isGrounded = Physics.Raycast(GroundCheck.position, -transform.up, 0.01f);
        isRunning = Input.GetButton("Run");
        float maxSpeed = isRunning || !isGrounded ? RunSpeed : WalkSpeed;
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * maxSpeed;

        // jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            needJump = true;
            fmodHelper.PlayOneshot(FMODEventJump);
        }

        // sound
        float stepDelay = 1f / (StepsPerSecond * (RunStepsMultiplier - 1f) / (RunSpeed - WalkSpeed) * (velocity.magnitude - WalkSpeed) + StepsPerSecond);
        if(isGrounded && velocity.magnitude > 1f && Time.time - lastStepTime > stepDelay)
        {
            lastStepTime = Time.time;
            fmodHelper.PlayOneshot(FMODEventStep);
        }
    }

    void FixedUpdate()
    {
        // get velocity
        velocity = characterController.velocity;
        Vector3 velocityLocal2D = transform.InverseTransformDirection(velocity);
        velocityLocal2D.y = 0f;

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;

            if(!wasGrounded)
            {
                lastStepTime = Time.time;

                fmodHelper.PlayOneshot(FMODEventLand);
            }
        }

        wasGrounded = isGrounded;

        // movement
        float maxAccel = (isGrounded ? (isRunning ? RunAccel : WalkAccel) : AirAccel);
        Vector3 acceleration = Vector3.ClampMagnitude(movement - Vector3.ClampMagnitude(velocityLocal2D, RunSpeed), 1) * maxAccel * Time.fixedDeltaTime;
        velocity += transform.TransformDirection(acceleration);

        // gravity
        velocity -= Vector3.Scale(GravitySystem.GravityScale, Physics.gravity) * Time.fixedDeltaTime;

        if(needJump)
        {
            needJump = false;
            velocity -= JumpAccel * GravitySystem.GravityScale;
        }

        characterController.Move(velocity * Time.fixedDeltaTime);
    }
}
