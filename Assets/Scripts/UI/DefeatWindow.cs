using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefeatWindow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Window _defeatWindow;


    private void OnEnable()
    {
        _player.IsDead += OpenWindow;
    }

    private void OnDisable()
    {
        _player.IsDead -= OpenWindow;
    }

    public void OpenWindow()
    {
        _defeatWindow.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel(Window window)
    {
        window.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
