using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Level _level;

    private Button _button;
    private bool _isLevelOpen;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {

    }
}
