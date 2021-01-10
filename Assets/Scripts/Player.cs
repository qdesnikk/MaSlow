using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private LevelGenerator _levelGenerator;

    private Transform _transform;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private int _coinsCount = 0;

    public event UnityAction Died;
    public event UnityAction Finished;

    private void OnEnable()
    {
        this.Finished += FinishTheLevel;
    }

    private void OnDisable()
    {
        this.Finished -= FinishTheLevel;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();

        _coinsText.text = _coinsCount.ToString();
    }

    private void FinishTheLevel()
    {
        float jumpForce = 15;
        _rigidBody.AddForce(new Vector2(1f, 0.7f) * jumpForce, ForceMode2D.Impulse);
        _transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Finish finish))
        {
            Finished?.Invoke();
            Debug.Log("finish level");
        }
        else if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _coinsCount++;
            _coinsText.text = _coinsCount.ToString();
            PlayerPrefs.SetInt("CoinsCount", _coinsCount);
            coin.PickUp();
        }
        else if (collision.gameObject.TryGetComponent(out Obstackle obstackle))
        {
            _animator.Play("Death");
            Died?.Invoke();
            Debug.Log(obstackle);
        }
    }
}
