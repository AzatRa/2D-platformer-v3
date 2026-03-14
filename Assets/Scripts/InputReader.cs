using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputAction _playerInputs;
    [SerializeField] private InputAction _jumpInputs;

    public event Action OnJump;
    public event Action OnJumpRelease;

    private Vector2 _moveDirection;

    private void OnEnable()
    {
        _playerInputs.Enable();
        _jumpInputs.Enable();
    }

    private void Update()
    {
        _moveDirection = _playerInputs.ReadValue<Vector2>();

        if (_jumpInputs.WasPressedThisFrame())
        {
            OnJump?.Invoke();
        }

        if (_jumpInputs.WasReleasedThisFrame())
        {
            OnJumpRelease?.Invoke();
        }
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
        _jumpInputs.Disable();
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }
}
