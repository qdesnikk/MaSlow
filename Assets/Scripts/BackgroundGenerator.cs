using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private float _backgroundLength;
    [SerializeField] private LevelChanger _levelChanger;

    private Level _currentLevel;
    private List<GameObject> _container = new List<GameObject>();
    private int _imagesCount = 3;
    private Vector3 _nextPosition;
    private int _firstImage = 0;

    private void Start()
    {
        _currentLevel = _levelChanger.GetCurrentLevel();
        _background = _currentLevel.Background;

        _nextPosition = new Vector3(Camera.main.transform.position.x - _backgroundLength, 7f, 0f);

        for (int i = 0; i < _imagesCount; i++)
        {
            var back = Instantiate(_background, _nextPosition, Quaternion.identity, this.transform);
            _container.Add(back);

            _nextPosition = GetNextPosition();
        }
    }

    private void Update()
    {
        TrySetNextImage();
    }

    private void TrySetNextImage()
    {
        if (Camera.main.transform.position.x > _container[_firstImage].transform.position.x + _backgroundLength)
        {
            _container[_firstImage].transform.position = _nextPosition;

            if (_firstImage < _container.Count - 1)
                _firstImage++;
            else
                _firstImage = 0;

            _nextPosition = GetNextPosition();
        }
    }

    private Vector3 GetNextPosition()
    {
        return new Vector3(_nextPosition.x + _backgroundLength, _nextPosition.y, _nextPosition.z);
    }
}
