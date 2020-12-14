using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void OpenWindow(Window window)
    {
        window.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame(Window window)
    {
        window.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
