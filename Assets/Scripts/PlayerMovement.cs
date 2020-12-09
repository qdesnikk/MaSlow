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
    [SerializeField] private Image _whiteScreen;

    private PlayerInput _input;
    private Rigidbody2D _rigidBody;
    private bool _isGrounded;
    private Animator _animator;


    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Jump.performed += ctx => Jump();
        _input.Player.Slide.performed += ctx => Slide();
        _input.Player.SlowMotion.performed += ctx => SlowMotion();

        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
            _animator.Play("Jump");
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Slide()
    {
        if (_isGrounded)
        {
            _animator.Play("Slide");
            _rigidBody.AddForce(Vector2.right * _slideForce, ForceMode2D.Impulse);
        }
    }

    private void SlowMotion()
    {
        //_whiteScreen.gameObject.SetActive(true);
        _whiteScreen.DOFade(0.5f, 0.1f);
        _whiteScreen.DOFade(0f, 1f);

        StartCoroutine(SlowMotionTimer());
    }

    private IEnumerator SlowMotionTimer()
    {
        Time.timeScale = 0.4f;

        while (Time.timeScale < 1f)
        {
            Time.timeScale += 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

            yield return new WaitForSeconds(0.2f);
        }
        //_whiteScreen.gameObject.SetActive(false);
    }
}
