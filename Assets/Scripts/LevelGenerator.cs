using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GridObject[] _bottomTemplates;
    [SerializeField] GridObject[] _topTemplates;
    [SerializeField] GridObject[] _obstacleTemplates;
    [SerializeField] private Player _player;
    [SerializeField] private float _viewDistance;
    [SerializeField] private Vector2 _cellSize;

    private HashSet<Vector2Int> _collisionsMatrix = new HashSet<Vector2Int>();
    [SerializeField] int countBetweenObstackles = 0;

    private void Update()
    {
        FillDistance(_player.transform.position, _viewDistance);
    }

    private void FillDistance(Vector2 center, float viewDistance)
    {
        var cellCountOnAxis = (int)(viewDistance / _cellSize.x);
        var fillAreaCenter = WorldToGridPosition(center);

        for (int x = -cellCountOnAxis; x < cellCountOnAxis; x++)
        {
            TryCreateOnLayer(GridLayer.KitchenFirstFloor, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateOnLayer(GridLayer.KitchenSecondFloor, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateOnLayer(GridLayer.ObstackleFirstFloor, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateOnLayer(GridLayer.ObstackleSecondFloor, fillAreaCenter + new Vector2Int(x, 0));
        }
    }

    private void TryCreateOnLayer(GridLayer layer, Vector2Int gridPosition)
    {
        GridObject template;
        gridPosition.y = (int)layer;

        if (_collisionsMatrix.Contains(gridPosition))
            return;
        else
            _collisionsMatrix.Add(gridPosition);

        if (layer == GridLayer.KitchenFirstFloor)
        {
            template = GetRandomKitchenFrstFloorTemplate();
            countBetweenObstackles++;
        }
        else if (layer == GridLayer.KitchenSecondFloor)
        {
            template = GetRandomKitchenSecondFloorTemplate();
        }
        else
        {
            return;
        }

        if (countBetweenObstackles >= 5)
        {
            CreateRandomObstackle(gridPosition);
            countBetweenObstackles = 0;
        }

        if (template == null)
            return;

        var position = GridToWorldPosition(gridPosition);

        Instantiate(template, position, Quaternion.identity, transform);
    }

    private void CreateRandomObstackle(Vector2Int gridPosition)
    {
        var template = GetRandomObstacleTemplate();

        gridPosition.y = (int)template.Layer;

        var position = GridToWorldPosition(gridPosition);

        Instantiate(template, position, Quaternion.identity, transform);
    }

    private GridObject GetRandomObstacleTemplate()
    {
        return _obstacleTemplates[Random.Range(0, _obstacleTemplates.Length)];
    }

    private GridObject GetRandomKitchenFrstFloorTemplate()
    {
        return _bottomTemplates[Random.Range(0, _bottomTemplates.Length)];
    }

    private GridObject GetRandomKitchenSecondFloorTemplate()
    {
        int chance = Random.Range(0, 100);

        if (chance >= 80)
            return _topTemplates[Random.Range(0, _topTemplates.Length)];
        else
            return null;
    }

    private Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector2(
            gridPosition.x * _cellSize.x,
            gridPosition.y * _cellSize.y);
    }

    private Vector2Int WorldToGridPosition(Vector2 worldPosition)
    {
        return new Vector2Int(
            (int)(worldPosition.x / _cellSize.x),
            (int)(worldPosition.y / _cellSize.y));
    }
}
