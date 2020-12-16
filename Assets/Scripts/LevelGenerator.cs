using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GridObject[] _bottomTemplates;
    [SerializeField] GridObject[] _topTemplates;
    [SerializeField] GridObject[] _obstacleTemplates;
    [SerializeField] GridObject _startTemplate;
    [SerializeField] GridObject _finishTemplate;
    [SerializeField] private Player _player;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private float _viewDistance;
    [SerializeField] private Vector2 _cellSize;

    private HashSet<Vector2Int> _collisionsMatrix = new HashSet<Vector2Int>();
    private bool _isStartCreated = false;
    private bool _isFinishCreated = false;
    [SerializeField] private int _totalCountBetweenObstackles = 5;
    [SerializeField] private int _currentCountBetweenObstackles = 0;
    [SerializeField] private int _countObstacklesInLevel = 5;
    [SerializeField] private int _levelLength;

    private void Awake()
    {
        _currentCountBetweenObstackles = _totalCountBetweenObstackles * 2;
        _levelLength = _totalCountBetweenObstackles * _countObstacklesInLevel + _currentCountBetweenObstackles;

        _progressBar.maxValue = _levelLength - (int)(_viewDistance / _cellSize.x * 2);
        _progressBar.value = _progressBar.maxValue;
    }

    private void Update()
    {
        FillDistance(_player.transform.position, _viewDistance);
        _progressBar.value = Mathf.Lerp(_progressBar.value, _levelLength, 0.5f * Time.deltaTime);
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
            template = GetRandomKitchenFrstFloorTemplate();
            _currentCountBetweenObstackles--;
            _levelLength--;
        }
        else if (layer == GridLayer.KitchenSecondFloor)
        {
            template = GetRandomKitchenSecondFloorTemplate();
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
            obstackleTemplate = GetRandomObstacleTemplate();
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
