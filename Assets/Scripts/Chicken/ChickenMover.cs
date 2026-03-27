using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChickenMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;

    public event Action GoLeft;
    public event Action GoRight;

    private Rigidbody2D _rigidbody;
    private Vector2 _targetPosition;
    private float _minDistance = 0.1f;
    private float _minSpeed = 0.01f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 delta = _targetPosition - _rigidbody.position;
        float distance = delta.magnitude;
        Vector2 direction = delta.normalized;
        Vector2 velocity = _rigidbody.linearVelocity;

        if (distance > _minDistance)
        {
            velocity.x = direction.x * _moveSpeed;

            _rigidbody.linearVelocity = velocity;
        }
        else
        {
            velocity.x = 0;
        }

        _rigidbody.linearVelocity = velocity;

        InvokeDirection();
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _targetPosition = new Vector2(targetPosition.x, _rigidbody.position.y);
    }

    public bool IsMoving()
    {
        return Mathf.Abs(_rigidbody.linearVelocity.x) > _minSpeed;
    }

    private void InvokeDirection()
    {
        if (transform.position.x < _targetPosition.x)
        {
            GoRight?.Invoke();
        }
        else
        {
            GoLeft?.Invoke();
        }
    }
}
