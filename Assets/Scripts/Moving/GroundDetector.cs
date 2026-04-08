using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private const string Ground = "Ground";

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _edgeCheck;
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private bool _facingRight = true;

    public bool IsGround { get; private set; }
    public bool IsEdge { get; private set; }

    private LayerMask _groundMask;
    private Vector2 _edgeCheckPosition;

    private void Awake()
    {
        _groundMask = LayerMask.GetMask(Ground);
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckEdge();
    }

    public void SetDirection(bool facingRight)
    {
        _facingRight = facingRight;
    }

    private void CheckGround()
    {
        IsGround = Physics2D.OverlapCircle(_groundCheck.position, _radius, _groundMask);
    }

    private void CheckEdge()
    {
        Vector2 direction;

        if (_facingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        
        _edgeCheckPosition = (Vector2)_groundCheck.position + direction * Mathf.Abs(_edgeCheck.localPosition.x);
        bool hasGroundAhead = Physics2D.OverlapCircle(_edgeCheckPosition, _radius, _groundMask);

        IsEdge = IsGround && !hasGroundAhead;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_edgeCheckPosition, _radius);
    }
}