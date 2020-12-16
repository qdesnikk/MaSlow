using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private LevelGenerator _levelGenerator;

    private Transform _transform;
    private Animator _animator;
    private int _coinsCount = 0;

    public event UnityAction IsDead;
    public event UnityAction IsFinished;

    private void OnEnable()
    {
        this.IsFinished += FinishTheLevel;
    }

    private void OnDisable()
    {
        this.IsFinished -= FinishTheLevel;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _coinsText.text = _coinsCount.ToString();
    }

    private void FinishTheLevel()
    {
        _transform.DORotate(new Vector3(0, 0, 180), 1);
        _transform.DOMove(_transform.position + new Vector3(3,3,0), 2);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Finish finish))
        {
            IsFinished?.Invoke();
            Debug.Log("finish level");
        }
        else if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _coinsCount++;
            _coinsText.text = _coinsCount.ToString();
            Destroy(coin.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out Obstackle obstackle))
        {
            //_animator.Play("Death");
            //IsDead?.Invoke();
            Debug.Log(obstackle);
        }
    }
}
