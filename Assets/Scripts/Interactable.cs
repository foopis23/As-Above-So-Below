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
    private GameObject player;

    public abstract void OnInteract();
    public abstract void OnLookStart();
    public abstract void OnLookStop();
    
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {

    }

    public virtual void OnLook()
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

}
