using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GroundDetector : MonoBehaviour
{
    private const string Ground = "Ground";

    [SerializeField] private float _rayDistance = 0.1f;
    [SerializeField] private int _rayCount = 5;

    public bool IsGround { get; private set; }

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private LayerMask _groundMask;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _groundMask = LayerMask.GetMask(Ground);
    }

    private void FixedUpdate()
    {
        IsGround = CheckGround();
    }

    private bool CheckGround()
    {
        Bounds bounds = _collider.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minY = bounds.min.y;

        float rayStep = (maxX - minX) / (_rayCount - 1);

        for (int i = 0; i < _rayCount; i++)
        {
            Vector2 rayVectorPosition = new Vector2(minX + rayStep * i, minY);
            RaycastHit2D hit = Physics2D.Raycast(rayVectorPosition, Vector2.down, _rayDistance, _groundMask);
            Debug.DrawRay(rayVectorPosition, Vector2.down * _rayDistance, Color.red);

            if (hit.collider != null)
                return true;
        }

        return false;
    }
}