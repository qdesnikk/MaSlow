using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/Create new level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private Image _background;
    [SerializeField] private List<GridObject> _kithen;
    [SerializeField] private List<Obstackle> _obstackles;
    [SerializeField] private int _obstacklesCount;
    [SerializeField] private float _speed;
    [SerializeField] private bool isOpen;

    public int ObstacklesCount => _obstacklesCount;
    public List<Obstackle> Obstackles => _obstackles;

}
