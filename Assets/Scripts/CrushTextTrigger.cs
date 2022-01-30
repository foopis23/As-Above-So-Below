using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrushTextTrigger : MonoBehaviour
{
    // Editor Fields
    public TextMeshProUGUI HintTextObject;

    void OnTriggerEnter()
    {
        HintTextObject.text = "[RMB] Crush";
    }
}
