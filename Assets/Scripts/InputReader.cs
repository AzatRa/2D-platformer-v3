using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputAction _moveInputs;
    [SerializeField] private InputAction _jumpInputs;
    [SerializeField] private InputAction _attackInputs;

    public event Action OnJump;
    public event Action OnJumpRelease;
    public event Action OnAttack;
    public event Action OnAttackRelease;

    private Vector2 _moveDirection;

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
            OnJump?.Invoke();

        if (_jumpInputs.WasReleasedThisFrame())
            OnJumpRelease?.Invoke();

        if (_attackInputs.WasPressedThisFrame())
            OnAttack?.Invoke();

        if (_attackInputs.WasReleasedThisFrame())
            OnAttackRelease?.Invoke();

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
