using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Interactable
{
    // Editor Fields
    public float HoldDistance = 1.5f;

    // Public Properties
    public bool IsHeld { get; private set; }

    // Private Fields
    private Rigidbody rigidbody;
    private GravityController gravityController;
    private Camera camera;
    private bool justPickedUp;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rigidbody = GetComponent<Rigidbody>();
        gravityController = GetComponent<GravityController>();
        camera = Camera.main;
        IsHeld = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(IsHeld)
        {
            Vector3 holdPosition = camera.transform.position + camera.transform.up * -0.25f + camera.transform.forward * HoldDistance;

            if((!justPickedUp && Input.GetButtonDown("Interact")) || (holdPosition - transform.position).magnitude > 3f)
            {
                IsHeld = false;
                player.GetComponent<PlayerController>().IsHoldingObject = false;
                gravityController.enabled = true;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }

            justPickedUp = false;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(IsHeld)
        {
            Vector3 holdPosition = camera.transform.position + camera.transform.up * -0.25f + camera.transform.forward * HoldDistance;
            rigidbody.MovePosition(holdPosition);
            rigidbody.MoveRotation(player.transform.rotation);
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

        IsHeld = true;
        player.GetComponent<PlayerController>().IsHoldingObject = true;
        gravityController.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Held Cube");

        justPickedUp = true;
    }
}
