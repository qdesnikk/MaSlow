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
    private bool isAlive = true;

    public event UnityAction<Player> PlayerIsDead;
    public event UnityAction LevelIsFinished;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _coinsText.text = _coinsCount.ToString();
    }

    private void Update()
    {
        if (!isAlive)
            PlayerIsDead?.Invoke(this);
    }

    private void FinishTheLevel()
    {
        //_transform.DORotate(new Vector3(0, 0, 40), 1);
        LevelIsFinished?.Invoke();
        Debug.Log("finish level");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Finish finish))
        {
            FinishTheLevel();
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
            isAlive = false;
            Debug.Log(obstackle);
        }
    }
}
