using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    public Animator buttonAnimator;
    public string buttonAnimationName;
    
    public string buttonText;
    public UnityEvent interactionEvent;

    public bool IsInteractable()
    {
        return true;
    }

    public void OnLookStart()
    {
        // TODO: Render outline or some shit
        Debug.Log("OnLookStart");
    }

    public void OnLook()
    {
        
    }

    public void OnLookStop()
    {
        // TODO: Stop rendering outline or something
        Debug.Log("OnLookStop");
    }

    public void OnInteract()
    {
        if (buttonAnimator != null)
            buttonAnimator.Play(buttonAnimationName);
        interactionEvent.Invoke();
        Debug.Log("On Interact");
    }
}
