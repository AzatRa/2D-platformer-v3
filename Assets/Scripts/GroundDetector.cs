using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector : MonoBehaviour
{
    private const string Ground = "Ground";

    [SerializeField] private float _rayDistance = 0.1f;
    [SerializeField] private int _rayCount = 5;

    public bool IsGround { get; private set; }
    public bool IsEdge { get; private set; }

    private Collider2D _collider;
    private LayerMask _groundMask;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _groundMask = LayerMask.GetMask(Ground);
    }

    private void FixedUpdate()
    {
        IsGround = CheckGround();
        IsEdge = CheckEdge();
    }

    private bool CheckGround()
    {
        if (Calculate() > 0)
            return true;
        else
            return false;
    }

    private bool CheckEdge()
    {
        if (Calculate() != _rayCount)
            return true;
        else
            return false;
    }

    private int Calculate()
    {
        int value = 0;
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
                value++;
        }

        return value;
    }
}