using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;

    private Slider _progressBar;

    private void Start()
    {
        _progressBar = GetComponent<Slider>();

        _progressBar.maxValue = _levelGenerator.LevelLength;
        _progressBar.value = _progressBar.maxValue;
    }

    private void Update()
    {
        _progressBar.value = Mathf.Lerp(_progressBar.value, _levelGenerator.LevelLength, 0.5f * Time.deltaTime);
    }


}
