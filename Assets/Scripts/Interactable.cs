using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interactable : MonoBehaviour
{
    // Editor Fields
    public GameObject OutlineObject;
    public string HintText;

    // Public Properties
    public bool IsLookedAt { get; private set; }

    private bool _isInteractable;
    public bool IsInteractable {
        get {
            return _isInteractable && !player.GetComponent<PlayerController>().IsHoldingObject;
        }
        set {
            _isInteractable = value;
        }
    }

    // Private Fields
    protected GameObject player;
    protected TextMeshProUGUI hintTextObject;
    
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hintTextObject = GameObject.FindGameObjectWithTag("Hint Text")?.GetComponent<TextMeshProUGUI>();
        IsLookedAt = false;
        IsInteractable = true;
    }

    protected virtual void Update()
    {
        if(OutlineObject != null)
        {
            if(IsLookedAt && IsInteractable)
            {
                OutlineObject.SetActive(true);
            }
            else
            {
                OutlineObject.SetActive(false);
            }
        }
    }

    protected virtual void FixedUpdate() {}

    public virtual void OnLook() {}

    public virtual void OnLookStart()
    {
        IsLookedAt = true;

        if(IsInteractable && hintTextObject != null)
        {
            hintTextObject.text = HintText;
            hintTextObject.enabled = true;
        }
    }

    public virtual void OnLookStop()
    {
        IsLookedAt = false;

        if(IsInteractable && hintTextObject != null)
        {
            hintTextObject.enabled = false;
        }
    }

    public virtual void OnInteract() {}
}
