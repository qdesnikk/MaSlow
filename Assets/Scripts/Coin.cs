using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pickUpEffect;
    [SerializeField] private ParticleSystem _shineEffect;

    public void OnPickUp()
    {
        _pickUpEffect.Play();
        _shineEffect.gameObject.SetActive(false);
        this.transform.DOScale(Vector3.zero, 0.5f);
        Destroy(this.gameObject, 1f);
    }
}
