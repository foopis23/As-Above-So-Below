using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNameTrigger : MonoBehaviour
{
    public Animator fadeAnimator;
    public TMP_Text textElement;
    public float transitionSpeed;
    public string levelName;

    public bool hasPlayed = false;

    private void Start()
    {
        textElement.text = levelName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;
        fadeAnimator.gameObject.SetActive(true);
        fadeAnimator.Play("Fade");
        hasPlayed = true;
        StartCoroutine(OnAnimationFinished());
    }

    private IEnumerator OnAnimationFinished()
    {
        yield return new WaitForSeconds(transitionSpeed);
        fadeAnimator.gameObject.SetActive(false);
    }
}
