using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public string openAnimationName;
    public string closeAnimationName;
    
    public string doorOpenSoundName;
    public string doorCloseSoundName;

    private FMODHelper _fmodHelper;
    private bool _isOpen = false;

    private void Start()
    {
        _fmodHelper = new FMODHelper(new string[]{doorOpenSoundName, doorCloseSoundName});
        _fmodHelper.FMODEvents[doorCloseSoundName]
            .set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        
        _fmodHelper.FMODEvents[doorOpenSoundName]
            .set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
    }
    
    public void OpenDoor()
    {
        if (_isOpen) return;
        _isOpen = true;
        doorAnimator.Play(openAnimationName);
        _fmodHelper.PlayOneshot(doorOpenSoundName);
    }

    public void CloseDoor()
    {
        if (!_isOpen) return;
        _isOpen = false;
        
        doorAnimator.Play(closeAnimationName);
        _fmodHelper.PlayOneshot(doorCloseSoundName);
    }
}
