using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private CountDown _countDown;

    private Slider _progressBar;

    private void OnEnable()
    {
        _countDown.MoveStarted += GetMaxValue;
    }

    private void OnDisable()
    {
        _countDown.MoveStarted -= GetMaxValue;
    }

    private void Start()
    {
        _progressBar = GetComponent<Slider>();
    }

    private void Update()
    {
        _progressBar.value = Mathf.Lerp(_progressBar.value, _levelGenerator.LevelLength, 0.5f * Time.deltaTime);
    }

    private void GetMaxValue()
    {
        _progressBar.maxValue = _levelGenerator.LevelLength;
        _progressBar.value = _progressBar.maxValue;
    }
}
