using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pickUpEffect;
    [SerializeField] private ParticleSystem _shineEffect;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        float movementRange = 1f;
        float targetX = Random.Range(this.transform.position.x - movementRange, this.transform.position.x + movementRange);
        float targetY = Random.Range(this.transform.position.y, this.transform.position.y + movementRange);
        Vector3 targetPosition = new Vector3(targetX, targetY, this.transform.position.z);
        this.transform.DOMove(targetPosition, 1f).SetLoops(-1, LoopType.Yoyo);
        //this.transform.DOShakePosition(20f, 1f, 3);
    }

    public void PickUp()
    {
        _pickUpEffect.Play();
        _shineEffect.gameObject.SetActive(false);
        this.transform.DOScale(Vector3.zero, 0.5f);
        Destroy(this.gameObject, 1f);
    }
}
