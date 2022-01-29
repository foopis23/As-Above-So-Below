using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool IsInteractable();
    void OnLookStart();
    void OnLook();
    void OnLookStop();
    void OnInteract();
}
