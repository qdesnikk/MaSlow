using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAction : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Window _finishWindow;
    [SerializeField] private ParticleSystem _finishParticle;

    private void OnEnable()
    {
        _player.LevelFinished += ShowCongratulations;
    }

    private void OnDisable()
    {
        _player.LevelFinished -= ShowCongratulations;
    }

    private void ShowCongratulations()
    {
        _finishParticle.Play();
        Invoke("ShowFinishWindow", 3f);
    }

    private void ShowFinishWindow()
    {
        _finishWindow.OpenWindow();
    }
}
