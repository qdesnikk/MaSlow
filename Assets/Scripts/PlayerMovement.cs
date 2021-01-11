using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slideForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private ParticleSystem _slideParticle;
    [SerializeField] private CountDown _countDown;

    private PlayerInput _input;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _isGrounded;
    private float _slideCountdown = 0;
    private bool _canMove = false;

    private void OnEnable()
    {
        _countDown.MoveStarted += AllowToMove;
    }

    private void OnDisable()
    {
        _countDown.MoveStarted -= AllowToMove;
        _input.Disable();
    }

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Jump.performed += ctx => Jump();
        _input.Player.Slide.performed += ctx => Slide();

        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_canMove)
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
        if (_isGrounded && _canMove)
        {
            _animator.Play("Jump");
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Slide()
    {
        if (_isGrounded && _canMove && _slideCountdown <= 0)
        {
            _slideParticle.Play();
            _animator.Play("Slide");
            _rigidBody.AddForce(Vector2.right * _slideForce, ForceMode2D.Impulse);

            StartCoroutine(SlideCountdownTimer());
        }
    }

    private void AllowToMove()
    {
        _canMove = true;
        _animator.Play("Run");
    }

    private IEnumerator SlideCountdownTimer()
    {
        _slideCountdown = 5;
        WaitForSeconds waitTime = new WaitForSeconds(1f);

        while (_slideCountdown > 0)
        {
            _slideCountdown--;

            yield return waitTime;
        }
    }
}
