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
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
