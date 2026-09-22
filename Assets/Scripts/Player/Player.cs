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
    [SerializeField] private Particler _particler;
    [SerializeField] private float _coyoteTime = 0.15f;

    private Vector2 _moveDirection;
    private float _lastGroundedTime;

    private void Start()
    {
        _inputReader.Jumping += Jumping;
        _inputReader.JumpReleased += JumpReleased;
        _inputReader.Attacking += Attacking;
        _health.Changed += HealthChanged;
        _health.Died += Died;
        _attacker.Attacked += Attacked;
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

        bool _isRunning = false;
        bool _isJumping = false;
        bool _isFalling = false;

        _visualizer.ResetAnimation();

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
        _inputReader.Jumping -= Jumping;
        _inputReader.JumpReleased -= JumpReleased;
        _inputReader.Attacking -= Attacking;
        _health.Changed -= HealthChanged;
        _health.Died -= Died;
        _attacker.Attacked -= Attacked;
    }

    private void Jumping()
    {
        if (Time.time - _lastGroundedTime <= _coyoteTime)
        {
            _jumper.Jump();
        }
    }

    private void JumpReleased()
    {
        _jumper.JumpCut();
    }

    private void Attacking()
    {
        _attacker.Attack();
    }

    private void Attacked()
    {
        _particler.Attack();
        _visualizer.SwitchAnimationAttack();
    }

    private void HealthChanged(int health, int amount)
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

    private void Died()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(_visualizer.GetAnimatorStateInfo().length);
        Destroy(gameObject);
    }
}