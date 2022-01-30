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

    public UnityEvent interactionEvent;
    
    public override void OnInteract()
    {
        base.OnInteract();
        
        buttonAnimator.Play(buttonAnimationName);
        interactionEvent.Invoke();
        IsInteractable = false;

        StartCoroutine(PressedDown());
    }

    private IEnumerator PressedDown()
    {
        yield return new WaitForSeconds(pressLength);
        
        buttonAnimator.Play(buttonReleaseAnimation);
        IsInteractable = true;
    }
}
