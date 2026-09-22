using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputAction _moveInputs;
    [SerializeField] private InputAction _jumpInputs;
    [SerializeField] private InputAction _attackInputs;

    private Vector2 _moveDirection;

    public event Action Jumping;
    public event Action JumpReleased;
    public event Action Attacking;
    public event Action AttackReleased;

    private void OnEnable()
    {
        _moveInputs.Enable();
        _jumpInputs.Enable();
        _attackInputs.Enable();
    }

    private void Update()
    {
        _moveDirection = _moveInputs.ReadValue<Vector2>();

        if (_jumpInputs.WasPressedThisFrame())
            Jumping?.Invoke();

        if (_jumpInputs.WasReleasedThisFrame())
            JumpReleased?.Invoke();

        if (_attackInputs.WasPressedThisFrame())
            Attacking?.Invoke();

        if (_attackInputs.WasReleasedThisFrame())
            AttackReleased?.Invoke();

    }

    private void OnDisable()
    {
        _moveInputs.Disable();
        _jumpInputs.Disable();
        _attackInputs.Disable();
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }
}
