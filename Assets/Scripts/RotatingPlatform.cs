using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float RotationSpeed = 100;

    private Quaternion desiredRotation;

    void Start()
    {
        desiredRotation = transform.rotation;
    }

    void Update()
    {
        float angle = Quaternion.Angle(transform.rotation, desiredRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, Mathf.Min(Time.deltaTime * RotationSpeed, angle));

        if(Input.GetKeyDown(KeyCode.R))
        {
            RotatePlatform();
        }
    }

    public void RotatePlatform()
    {
        desiredRotation *= Quaternion.Euler(0f, 90f, 0f);
    }
}
