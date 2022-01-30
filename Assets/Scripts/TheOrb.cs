using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrb : Interactable
{
    // Editor Fields
    public GameObject ModelObject;
    public float PickupCrushDelay;
    
    public string FMODEventGrab;
    public string FMODEventCrush;

    // Public Properties
    public bool IsHeld { get; private set; }

    // Private Fields
    FMODHelper fmodHelper;
    private float pickupTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        fmodHelper = new FMODHelper(new string[] { FMODEventGrab, FMODEventCrush });
        IsHeld = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(IsHeld)
        {
            if(Time.time - pickupTime > PickupCrushDelay && Input.GetButtonDown("Crush"))
            {
                IsHeld = false;
                player.GetComponent<PlayerController>().IsHoldingObject = false;
                player.GetComponent<PlayerController>().HeldObject = null;
                ModelObject.SetActive(true);
                fmodHelper.PlayOneshot(FMODEventCrush);

                GravitySystem.GravityScale = new Vector3(0, -1, 0);
            }
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

        IsHeld = true;
        player.GetComponent<PlayerController>().IsHoldingObject = true;
        player.GetComponent<PlayerController>().HeldObject = gameObject;
        ModelObject.SetActive(false);
        fmodHelper.PlayOneshot(FMODEventGrab);
        pickupTime = Time.time;

        GravitySystem.GravityScale = new Vector3(0, 1, 0);
    }
}
