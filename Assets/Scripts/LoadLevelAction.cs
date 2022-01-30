using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAction : MonoBehaviour
{
    public int levelIndex;

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelAfter());
    }

    private IEnumerator LoadLevelAfter()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelIndex);
    }
}
