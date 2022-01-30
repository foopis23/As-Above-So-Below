using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelAction : MonoBehaviour
{
    public int levelIndex;

    void Start()
    {
        int lastLevel = PlayerPrefs.GetInt("LastScene", 0);
        if(levelIndex > lastLevel + 1)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

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
