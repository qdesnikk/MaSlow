using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected Image WindowBackground;

    public virtual void OpenWindow()
    {
        this.gameObject.SetActive(true);
        WindowBackground.DOFade(0.5f, 1f).From(0).SetUpdate(true);
        this.transform.DOScale(1, 0.5f).From(0).SetUpdate(true);
        Time.timeScale = 0;
    }

    public virtual void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public virtual void RestartLevel()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
