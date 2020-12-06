using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slideForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;

    private PlayerInput _input;
    private Rigidbody2D _rigidBody;
    private bool _isGrounded;


    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Jump.performed += ctx => Jump();
        _input.Player.Slide.performed += ctx => Slide();
        _input.Player.SlowMotion.performed += ctx => SlowMotion();

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    private void Move()
    {
        _rigidBody.transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Slide()
    {
        if (_isGrounded)
        {
            _rigidBody.AddForce(Vector2.right * _slideForce, ForceMode2D.Impulse);
        }
    }

    private void SlowMotion()
    {
        Time.timeScale = 0.3f;
        StartCoroutine(SlowMotionTimer());
    }

    private IEnumerator SlowMotionTimer()
    {
        while (Time.timeScale < 1f)
        {
            Time.timeScale += 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
