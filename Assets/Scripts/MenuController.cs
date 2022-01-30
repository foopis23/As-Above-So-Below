using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Animator FadeToBlackController;
    public Animator MainPanel;
    public Animator LevelSelectPanel;

    public void ShowMainPanel()
    {
        LevelSelectPanel.Play("MenuSlideOut");
        MainPanel.Play("MenuSlideIn");
    }

    public void ShowLevelSelectPanel()
    {
        LevelSelectPanel.Play("MenuSlideIn");
        MainPanel.Play("MenuSlideOut");
    }

    public void FadeToBlack()
    {
        FadeToBlackController.Play("FadeToBlack");
    }
}
