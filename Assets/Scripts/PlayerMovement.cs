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
    [SerializeField] private ParticleSystem _slideParticle;
    [SerializeField] private Text _countDownText;

    private PlayerInput _input;
    private Rigidbody2D _rigidBody;
    private bool _isGrounded;
    private Animator _animator;
    private int _countDownInSeconds = 4;
    private float _currentMoveSpeed;


    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Jump.performed += ctx => Jump();
        _input.Player.Slide.performed += ctx => Slide();
        _input.Player.SlowMotion.performed += ctx => SlowMotion();

        _slideParticle.Stop();

        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _currentMoveSpeed = 0;
        _countDownText.gameObject.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        if (_countDownInSeconds == 0)
        {
            _currentMoveSpeed = _moveSpeed;
            _countDownText.gameObject.SetActive(false);
        }

        Move();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    private void Move()
    {
        _rigidBody.transform.Translate(Vector3.right * _currentMoveSpeed * Time.deltaTime);
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
            _slideParticle.Play();
            _animator.Play("Slide");
            _rigidBody.AddForce(Vector2.right * _slideForce, ForceMode2D.Impulse);
        }
    }

    private void SlowMotion()
    {
        _whiteScreen.DOFade(0.6f, 0.1f);
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
    }

    private IEnumerator StartCountdown()
    {
        
        while (_countDownInSeconds > 0)
        {
            _countDownText.gameObject.SetActive(true);
            _countDownInSeconds--;
            _countDownText.text = _countDownInSeconds.ToString();

            yield return new WaitForSeconds(1f);
        }
    }
}
