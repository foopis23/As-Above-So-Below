using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PressurePlate : MonoBehaviour
{
    public Animator animator;
    public string downAnimationName;
    public string upAnimationName;
    public UnityEvent activateEvent;
    public UnityEvent deactivateEvent;

    public string FMODEventPress;

    private int _gravityItemsOnPlate;

    private FMODHelper fmodHelper;

    void Start()
    {
        fmodHelper = new FMODHelper(new string[] { FMODEventPress });
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<GravityController>()) return;
        if (_gravityItemsOnPlate == 0)
        {
            activateEvent.Invoke();
            animator.Play(downAnimationName);
            fmodHelper.PlayOneshot(FMODEventPress);
        }
            
        _gravityItemsOnPlate++;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<GravityController>()) return;
        _gravityItemsOnPlate--;

        if (_gravityItemsOnPlate == 0)
        {
            deactivateEvent.Invoke();
            animator.Play(upAnimationName);
            fmodHelper.PlayOneshot(FMODEventPress);
        }
    }
}
