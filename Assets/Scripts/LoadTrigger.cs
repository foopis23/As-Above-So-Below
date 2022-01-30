using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour
{
    public int nextSceneIndex;
    public float loadDelay;
    public Animator fadeToBlack;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
            fadeToBlack.Play("FadeToBlack");
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
