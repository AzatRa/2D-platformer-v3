using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 18f;
    [SerializeField] private float _jumpCutMultiplier = 0.5f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.y = _jumpForce;
        _rigidbody.linearVelocity = velocity;
    }

    public void JumpCut()
    {
        if (_rigidbody.linearVelocity.y > 0)
        {
            Vector2 velocity = _rigidbody.linearVelocity;
            velocity.y *= _jumpCutMultiplier;
            _rigidbody.linearVelocity = velocity;
        }
    }
}