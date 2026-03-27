using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ChickenAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _maxRunTimer = 2f;
    [SerializeField] private float _maxIdleTimer = 3f;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private ChickenVisualizer _visualizer;
    [SerializeField] private ChickenPatrol _patrol;
    [SerializeField] private ChickenMover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Particler _particler;

    private State _state;
    private float _timer;
    private Vector2 _runTargetPosition;
    private bool _hasTarget = false;
    private bool _isRunning = false;
    private bool _isDirectionChanging = false;
    private bool _isAttacking = false;

    private void Awake()
    {
        _state = _startingState;
    }

    private void Start()
    {
        _health.OnChange += OnHealthChange;
        _health.OnDied += OnDied;
        _playerDetector.OnDetect += OnPlayerDetect;
        _attacker.OnAttack += Attack;
        _attacker.OnAttackRelease += AttackStop;
        _mover.GoLeft += GoLeft;
        _mover.GoRight += GoRight;
    }

    private enum State
    {
        Idle,
        Run,
        Attack
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        switch (_state)
        {
            default:
            case State.Idle:
                Idle();
                break;

            case State.Run:
                Run();
                break;

            case State.Attack:
                Attack();
                break;
        }

        ResetRunAnimation();
    }

    private void OnDestroy()
    {
        _health.OnChange -= OnHealthChange;
        _health.OnDied -= OnDied;
        _playerDetector.OnDetect -= OnPlayerDetect;
        _attacker.OnAttack -= Attack;
        _mover.GoLeft -= GoLeft;
        _mover.GoRight -= GoRight;
    }

    private void Run()
    {
        if (!_hasTarget && _groundDetector.IsGround)
        {
            _runTargetPosition = _patrol.GetTargetPosition();
            ResetRun();
            _hasTarget = true;
            _isDirectionChanging = false;
        }

        if (!_isAttacking && _timer >= _maxRunTimer)
        {
            _mover.SetTargetPosition(transform.position);
            _state = State.Idle;
            _timer = 0;
            _hasTarget = false;
        }

        if (!_isAttacking && _groundDetector.IsEdge && !_isDirectionChanging)
        {
            _runTargetPosition = _patrol.ChangeDirection();
            _isDirectionChanging = true;
            ResetRun();
        }
    }

    private void ResetRun()
    {
        _mover.SetTargetPosition(_runTargetPosition);
    }

    private void ResetRunAnimation()
    {
        if (_isRunning != _mover.IsMoving())
        {
            _isRunning = _mover.IsMoving();
            _visualizer.SwitchAnimation(_isRunning);
        }
    }

    private void Idle()
    {
        if (_isRunning)
        {
            _isRunning = false;
            _visualizer.SwitchAnimation(_isRunning);
        }
        
        if (_timer >= _maxIdleTimer)
        {
            _state = State.Run;
            _timer = 0;
        }
    }

    private void GoLeft()
    {
        _visualizer.GoLeft();
        _playerDetector.GoLeft();
    }

    private void GoRight()
    {
        _visualizer.GoRight();
        _playerDetector.GoRight();
    }

    private void OnPlayerDetect(Vector2 playerPosition)
    {
        if (!_isAttacking)
        {
            _attacker.Go();
            _state = State.Attack;
        }

        _runTargetPosition = playerPosition;
    }

    private void Attack()
    {
        _isAttacking = true;
        _particler.EnableAttack();
        ResetRun();
    }

    private void AttackStop()
    {
        _isAttacking = false;
        _particler.DisableAttack();
        _state = State.Idle;
        _timer = 0;
    }

    private void OnHealthChange(int health, int amount)
    {
        if (amount < 0)
        {
            OnGetDamage();
        }

        if (amount > 0)
        {
            _particler.Regeneration();
        }
    }

    private void OnGetDamage()
    {
        _visualizer.SwitchAnimationHit();
    }

    private void OnDied()
    {
        StartCoroutine(DestroyChicken());
    }

    private IEnumerator DestroyChicken()
    {
        yield return new WaitForSecondsRealtime(_visualizer.GetAnimatorStateInfo().length);

        Destroy(gameObject);
    }
}
