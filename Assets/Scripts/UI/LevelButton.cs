using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Sprite _openImage;
    [SerializeField] private Sprite _closeImage;
    [SerializeField] private Level _level;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.interactable = false;
        _button.image.sprite = _closeImage;
    }

    private void Start()
    {
        _button.onClick.AddListener(OpenLevel);
    }

    public void Unlock()
    {
        _button.image.sprite = _openImage;
        _button.interactable = true;
    }

    public void OpenLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", _level.Number);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
