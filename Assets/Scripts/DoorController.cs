using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public string openAnimationName;
    public string closeAnimationName;
    
    public void OpenDoor()
    {
        doorAnimator.Play(openAnimationName);
    }

    public void CloseDoor()
    {
        doorAnimator.Play(closeAnimationName);
    }
}
