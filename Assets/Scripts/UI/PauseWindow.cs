using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseWindow : Window
{
    public void ResumeGame()
    {
        WindowBackground.DOFade(0f, 0.5f).SetUpdate(true);
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
