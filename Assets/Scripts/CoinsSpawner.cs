using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Player _player;

    private int _spawnCountdown;

    private void Awake()
    {
        StartCoroutine(SpawnCountdown());
    }

    private void Update()
    {
        if (_spawnCountdown <= 0)
        {
            TrySpawn();
            StartCoroutine(SpawnCountdown());
        }
    }

    private void TrySpawn()
    {
        Instantiate(_coin, _player.transform.position + new Vector3(20, 0, 0), Quaternion.identity);
    }

    private IEnumerator SpawnCountdown()
    {
        _spawnCountdown = Random.Range(3, 15);

        while (_spawnCountdown > 0)
        {
            _spawnCountdown--;

            yield return new WaitForSeconds(1f);
        }

    }
}
