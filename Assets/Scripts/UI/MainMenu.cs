using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenWindow(Window window)
    {
        window.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseWindow(Window window)
    {
        window.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
