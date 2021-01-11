using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _windowBackground;
    [SerializeField] private Text _coinsCount;

    private void Start()
    {
        _coinsCount.text = PlayerPrefs.GetInt("CoinsCount").ToString();
    }

    public void OpenWindow(Window window)
    {
        _windowBackground.DOFade(0.3f, 1f).From(0).SetUpdate(true);
        window.gameObject.SetActive(true);
        window.transform.DOScale(1, 0.5f).From(0).SetUpdate(true);
        Time.timeScale = 0;
    }

    public void CloseWindow(Window window)
    {
        _windowBackground.DOFade(0f, 0.5f).SetUpdate(true);
        window.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
