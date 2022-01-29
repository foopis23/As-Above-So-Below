using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    public Animator buttonAnimator;
    public string buttonAnimationName;
    
    public string buttonText;
    public UnityEvent interactionEvent;

    public override void OnInteract()
    {
        base.OnInteract();

        if (buttonAnimator != null)
            buttonAnimator.Play(buttonAnimationName);

        interactionEvent.Invoke();
    }
}
