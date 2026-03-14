using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerVisualizer _visualizer;
    [SerializeField] private Inventory _inventory;
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
            if (_mover.GetRigidbodyVelocity().y > 0)
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

            if (_moveDirection == Vector2.left)
                _visualizer.GoLeft();
            else
                _visualizer.GoRight();
        }
    }

    private void OnDestroy()
    {
        _inputReader.OnJump -= OnJump;
        _inputReader.OnJumpRelease -= OnJumpRelease;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Apple>(out var apple))
        {
            _inventory.Take(apple);
        }
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
}