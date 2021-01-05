using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefeatWindow : Window
{
    [SerializeField] private Player _player;
    
    private void OnEnable()
    {
        _player.Died += OpenWindow;
    }

    private void OnDisable()
    {
        _player.Died -= OpenWindow;
    }

}
