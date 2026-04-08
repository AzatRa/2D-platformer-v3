using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerVisualizer _visualizer;
    [SerializeField] private Health _health;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Damager _damager;
    [SerializeField] private Particler _particler;
    [SerializeField] private float _coyoteTime = 0.15f;

    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _isFalling = false;

    private Vector2 _moveDirection;
    private float _lastGroundedTime;

    private void Start()
    {
        _inputReader.OnJump += OnJump;
        _inputReader.OnJumpRelease += OnJumpRelease;
        _inputReader.OnAttack += OnAttack;
        _inputReader.OnAttackRelease += OnAttackRelease;
        _health.OnChanged += OnHealthChanged;
        _health.OnDied += OnDied;
        _attacker.OnAttack += OnAttackStart;
        _damager.OnTakedDamage += OnAttackRelease;
    }

    private void FixedUpdate()
    {
        _moveDirection = _inputReader.GetMoveDirection();
        _mover.Move(_moveDirection, _groundDetector.IsGround);
    }

    private void Update()
    {
        if (_groundDetector.IsGround)
            _lastGroundedTime = Time.time;

        ResetState();

        if (!_groundDetector.IsGround)
        {
            if (_mover.RigidbodyVelocity.y > 0)
            {
                _isJumping = true;
                _visualizer.SwitchAnimationJump(_isJumping);
            }
            else
            {
                _isFalling = true;
                _visualizer.SwitchAnimationFall(_isFalling);
            }

        }
        else
        {
            if (_moveDirection.x != 0)
            {
                _isRunning = true;
                _visualizer.SwitchAnimationRun(_isRunning);
            }
        }

        if (_moveDirection.x < 0)
            _visualizer.GoLeft();

        if (_moveDirection.x > 0)
            _visualizer.GoRight();
    }

    private void OnDestroy()
    {
        _inputReader.OnJump -= OnJump;
        _inputReader.OnJumpRelease -= OnJumpRelease;
        _inputReader.OnAttack -= OnAttack;
        _inputReader.OnAttackRelease -= OnAttackRelease;
        _health.OnChanged -= OnHealthChanged;
        _health.OnDied -= OnDied;
        _attacker.OnAttack -= OnAttackStart;
        _damager.OnTakedDamage -= OnAttackRelease;
    }

    private void OnJump()
    {
        if (Time.time - _lastGroundedTime <= _coyoteTime)
        {
            _jumper.Jump();
        }
    }

    private void OnJumpRelease()
    {
        _jumper.JumpCut();
    }

    private void ResetState()
    {
        _isRunning = false;
        _isJumping = false;
        _isFalling = false;

        _visualizer.ResetAnimation();
    }

    private void OnAttack()
    {
        _attacker.Enable();
    }

    private void OnAttackRelease()
    {
        _attacker.Disable();
        _particler.DisableAttack();
    }

    private void OnAttackStart()
    {
        _particler.EnableAttack();
    }

    private void OnHealthChanged(int health, int amount)
    {
        if (amount < 0)
        {
            _visualizer.SwitchAnimationHit();
        }

        if (amount > 0)
        {
            _particler.Regeneration();
        }
    }

    private void OnDied()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(_visualizer.GetAnimatorStateInfo().length);
        Destroy(gameObject);
    }
}