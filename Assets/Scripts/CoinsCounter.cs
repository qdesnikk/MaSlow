using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private Player _player;

    private int _coinsCount = 0;

    private void OnEnable()
    {
        _player.CoinCollecting += AddCoin;
    }

    private void OnDisable()
    {
        _player.CoinCollecting -= AddCoin;
    }

    private void Awake()
    {
        //PlayerPrefs.SetInt("CoinsCount", 0);
        _coinsCount = PlayerPrefs.GetInt("CoinsCount");
        _coinsText.text = _coinsCount.ToString();
    }

    private void AddCoin()
    {
        _coinsCount++;
        _coinsText.text = _coinsCount.ToString();
        PlayerPrefs.SetInt("CoinsCount", _coinsCount);
    }
}
