using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _bottomTemplates;
    [SerializeField] private GridObject[] _topTemplates;
    [SerializeField] private GridObject[] _obstacleTemplates;
    [SerializeField] private GridObject _startTemplate;
    [SerializeField] private GridObject _finishTemplate;
    [SerializeField] private Player _player;
    [SerializeField] private float _viewDistance;
    [SerializeField] private Vector2 _cellSize;

    private HashSet<Vector2Int> _collisionsMatrix = new HashSet<Vector2Int>();
    private bool _isStartCreated = false;
    private bool _isFinishCreated = false;
    private int _totalCountBetweenObstackles = 5;
    private int _currentCountBetweenObstackles = 0;
    private int _countObstacklesInLevel = 5;
    private int _levelLength;

    public int LevelLength => _levelLength;

    private void Awake()
    {
        _currentCountBetweenObstackles = _totalCountBetweenObstackles * 2;
        _levelLength = _totalCountBetweenObstackles * _countObstacklesInLevel;
    }

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
            if (!_isStartCreated)
            {
                CreateFlag(_startTemplate, fillAreaCenter + new Vector2Int(0, 0));
                _isStartCreated = true;
            }

            TryCreateKitchen(GridLayer.KitchenFirstFloor, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateKitchen(GridLayer.KitchenSecondFloor, fillAreaCenter + new Vector2Int(x, 0));

            if (_levelLength > 0)
            {
                TryCreateObstackle(fillAreaCenter + new Vector2Int(x, 0));
                TryCreateObstackle(fillAreaCenter + new Vector2Int(x, 0));
            }
            else if (!_isFinishCreated)
            {
                CreateFlag(_finishTemplate, fillAreaCenter + new Vector2Int(cellCountOnAxis, 0));
                _isFinishCreated = true;
            }
        }
    }

    private void TryCreateKitchen(GridLayer layer, Vector2Int gridPosition)
    {
        GridObject template;
        gridPosition.y = (int)layer;

        if (_collisionsMatrix.Contains(gridPosition))
            return;
        else
            _collisionsMatrix.Add(gridPosition);

        if (layer == GridLayer.KitchenFirstFloor)
        {
            template = GetRandomTemplate(_bottomTemplates);
            _currentCountBetweenObstackles--;
            _levelLength--;
        }
        else if (layer == GridLayer.KitchenSecondFloor)
        {
            template = GetRandomTemplate(_topTemplates);
        }
        else
        {
            return;
        }

        if (template == null)
            return;

        var position = GridToWorldPosition(gridPosition);
        Instantiate(template, position, Quaternion.identity, transform);
    }

    private void TryCreateObstackle(Vector2Int gridPosition)
    {
        GridObject obstackleTemplate;

        if ( _currentCountBetweenObstackles <= 0)
        {
            obstackleTemplate = GetRandomTemplate(_obstacleTemplates);
            gridPosition.y = (int)obstackleTemplate.Layer;
            _currentCountBetweenObstackles = _totalCountBetweenObstackles;
            _countObstacklesInLevel--;
        }
        else
        {
            return;
        }

        if (obstackleTemplate == null)
            return;

        var position = GridToWorldPosition(gridPosition);
        Instantiate(obstackleTemplate, position, Quaternion.identity, transform);
    }

    private void CreateFlag(GridObject flagTemplate, Vector2Int gridPosition)
    {
        gridPosition.y = (int)flagTemplate.Layer;
        var position = GridToWorldPosition(gridPosition);
        Instantiate(flagTemplate, position, Quaternion.identity, transform);
    }

    private GridObject GetRandomTemplate(GridObject[] templates)
    {
        var template = templates[Random.Range(0, templates.Length)];

        if (template.GetChance())
            return template;
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
