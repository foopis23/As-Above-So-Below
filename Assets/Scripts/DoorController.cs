using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public string openAnimationName;
    public string closeAnimationName;
    
    public string doorOpenSoundName;
    public string doorCloseSoundName;

    private FMODHelper _fmodHelper;

    private void Start()
    {
        _fmodHelper = new FMODHelper(new string[]{doorOpenSoundName, doorCloseSoundName});
    }
    
    public void OpenDoor()
    {
        doorAnimator.Play(openAnimationName);
        _fmodHelper.PlayOneshot(doorOpenSoundName);
    }

    public void CloseDoor()
    {
        doorAnimator.Play(closeAnimationName);
        _fmodHelper.PlayOneshot(doorCloseSoundName);
    }
}
