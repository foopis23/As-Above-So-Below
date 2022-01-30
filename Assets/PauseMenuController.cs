using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public bool Paused;

    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P) && !Input.GetKeyDown(KeyCode.Escape)) return;
        if (Paused)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        Paused = true;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void HidePauseMenu()
    {
        Paused = false;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
