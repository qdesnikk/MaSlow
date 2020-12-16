using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishWindow : MonoBehaviour
{
    public void OpenWindow(Window window)
    {
        window.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
