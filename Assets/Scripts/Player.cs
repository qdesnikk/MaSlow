using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    private Transform _transform;
    private Animator _animator;
    private int _coinsCount = 0;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _coinsText.text = _coinsCount.ToString();
    }

    private void FinishTheLevel()
    {

        _transform.DORotate(new Vector3(0, 0, 40), 1);
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
            _animator.Play("Death");
            Debug.Log(obstackle);
        }
    }
}
