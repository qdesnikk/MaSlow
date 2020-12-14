using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _dumping;
    [SerializeField] private Vector2 _speed = new Vector2(2f, 2f);

    private Vector3 _targetPosition;
    private float _lastX;

    private void Start()
    {
        _lastX = Mathf.RoundToInt(_target.position.x);
    }

    private void Update()
    {
        SetPosition(SetDirection());
    }

    private Direction SetDirection()
    {
        int currentX = Mathf.RoundToInt(_target.position.x);

        if (currentX >= _lastX)
        {
            _lastX = Mathf.RoundToInt(_target.position.x);
            return Direction.Right;
        }
        else
        {
            _lastX = Mathf.RoundToInt(_target.position.x);
            return Direction.Left;
        }
    }

    private void SetPosition(Direction direction)
    {
        _targetPosition = new Vector3(_target.position.x + _speed.x * (int)direction, _target.position.y + _speed.y, transform.position.z);
        Vector3 currentPosition = Vector3.Lerp(transform.position, _targetPosition, _dumping * Time.deltaTime);
        transform.position = currentPosition;
    }

    enum Direction
    {
        Left = -1,
        Right = 1
    }
}
