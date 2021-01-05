using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    private List<GridObject> _obstackles = new List<GridObject>();
    private int _currentLevel = 0;

    public List<GridObject> Obstackles => _obstackles;

    private void Awake()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        for (int i = 0; i < _levels[_currentLevel - 1].Obstackles.Count; i++)
        {
            _obstackles.Add(_levels[_currentLevel - 1].Obstackles[i]);
        }
    }
}
