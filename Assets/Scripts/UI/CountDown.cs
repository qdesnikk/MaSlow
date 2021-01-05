using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
public class CountDown : MonoBehaviour
{
    private int _currentCount = 3;
    private Text _countDownText;

    public event UnityAction LevelStarted;

    private void Awake()
    {
        _countDownText = GetComponent<Text>();

        StartCoroutine(CountdownTimer());
    }

    private void Update()
    {
        if (_currentCount < 1)
        {
            LevelStarted?.Invoke();
            _countDownText.gameObject.SetActive(false);
        }
    }

    private IEnumerator CountdownTimer()
    {
        _countDownText.gameObject.SetActive(true);

        while (_currentCount >= 1)
        {
            _countDownText.text = _currentCount.ToString();
            _countDownText.transform.DOScale(Vector3.zero, 1f).From(Vector3.one);
            _countDownText.DOFade(0f, 1f).From(1f);

            yield return new WaitForSeconds(1f);

            _currentCount--;
        }
    }
}
