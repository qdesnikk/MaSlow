using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _chance;

    public GridLayer Layer => _layer;
    //public int Chance => _chance;

    private void OnValidate()
    {
        //_chance = Mathf.Clamp(_chance, 1, 100);
    }

    public bool GetChance()
    {
        if (_chance >= Random.Range(0f, 100f))
            return true;
        else
            return false;
    }
}