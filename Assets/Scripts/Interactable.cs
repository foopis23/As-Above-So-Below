using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Editor Fields
    public GameObject OutlineObject;

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
    
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        IsLookedAt = false;
        IsInteractable = true;
    }

    protected virtual void Update()
    {
        Debug.Log(IsInteractable);
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

    public virtual void OnLook() {}

    public virtual void OnLookStart()
    {
        IsLookedAt = true;
    }

    public virtual void OnLookStop()
    {
        IsLookedAt = false;
    }

    public virtual void OnInteract() {}
}
