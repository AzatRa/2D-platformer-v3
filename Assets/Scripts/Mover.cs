using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _fallSpeed = 7f;

    public float MoveSpeed => _moveSpeed;
    public Vector2 MoveDirection => _currentMoveDirection;

    private Rigidbody2D _rigidbody;
    private Vector2 _currentMoveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 vector, bool onGround)
    {
        _currentMoveDirection = vector.normalized;

        float speed = _fallSpeed;

        if (onGround)
            speed = _moveSpeed;

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = vector.x * speed;

        _rigidbody.linearVelocity = velocity;
    }

    public Vector2 GetRigidbodyVelocity()
    {
        return _rigidbody.linearVelocity;
    }
}