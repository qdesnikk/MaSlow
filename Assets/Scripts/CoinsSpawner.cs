using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Player _player;
    [SerializeField] private int _coinsCountOnLevel;

    private void Start()
    {
        Invoke("Spawn", Random.Range(5, 15));
    }

    private void Spawn()
    {
        Instantiate(_coin, _player.transform.position + new Vector3(20, 0, 0), Quaternion.identity);
        _coinsCountOnLevel--;

        if (_coinsCountOnLevel > 0)
        {
            Invoke("Spawn", Random.Range(5, 15));
        }
    }

}
