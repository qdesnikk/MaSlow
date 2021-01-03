using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    private List<Obstackle> _obstackles = new List<Obstackle>();
    private int _current = 0;

    private void Awake()
    {
        for (int i = 0; i < _levels[_current].Obstackles.Count; i++)
        {
            _obstackles.Add(_levels[_current].Obstackles[i]);
        }
    }


}
