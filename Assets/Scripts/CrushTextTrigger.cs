using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrushTextTrigger : MonoBehaviour
{
    private TextMeshProUGUI hintTextObject;

    void Start()
    {
        hintTextObject = GameObject.FindGameObjectWithTag("Hint Text")?.GetComponent<TextMeshProUGUI>();
    }

    void OnTriggerEnter()
    {
        hintTextObject.text = "[RMB] Crush";
    }
}
