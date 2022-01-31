using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    public Animator buttonAnimator;
    public string buttonAnimationName;
    public string buttonReleaseAnimation;

    public float pressLength;
    
    public string buttonText;

    public string FMODEventPress;

    public UnityEvent interactionEvent;

    private FMODHelper fmodHelper;

    protected override void Start()
    {
        base.Start();

        fmodHelper = new FMODHelper(new string[] { FMODEventPress });
    }
    
    public override void OnInteract()
    {
        base.OnInteract();
        
        buttonAnimator.Play(buttonAnimationName);
        interactionEvent.Invoke();
        IsInteractable = false;
        hintTextObject.enabled = false;

        fmodHelper.PlayOneshot(FMODEventPress);

        StartCoroutine(PressedDown());
    }

    private IEnumerator PressedDown()
    {
        yield return new WaitForSeconds(pressLength);
        
        buttonAnimator.Play(buttonReleaseAnimation);
        IsInteractable = true;

        fmodHelper.PlayOneshot(FMODEventPress);
    }
}
