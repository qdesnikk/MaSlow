using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _coinUpEffect;

    private void Update()
    {
        //Destroy(this);
    }

    public void Destroy()
    {
        _coinUpEffect.Play();
        this.transform.DOScale(Vector3.zero, 0.5f);
    }
}
