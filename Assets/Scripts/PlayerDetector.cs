using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour
{
    private const string Player = "Player";

    [SerializeField] private float _rayDistance = 10f;
    [SerializeField] private int _rayCount = 5;

    public event Action<Vector2> OnDetect;

    private Collider2D _collider;
    private LayerMask _playerMask;
    private Vector2 _direction;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _playerMask = LayerMask.GetMask(Player);
    }

    private void FixedUpdate()
    {
        Bounds bounds = _collider.bounds;

        float centerX = bounds.center.x;
        float minY = bounds.min.y;
        float maxY = bounds.max.y;

        float rayStep = (maxY - minY) / (_rayCount - 1);

        for (int i = 0; i < _rayCount; i++)
        {
            Vector2 rayVectorPosition = new Vector2(centerX, minY + rayStep * i);
            RaycastHit2D hit = Physics2D.Raycast(rayVectorPosition, _direction, _rayDistance, _playerMask);
            Debug.DrawRay(rayVectorPosition, _direction * _rayDistance, Color.red);

            if (hit.collider != null)
                OnDetect?.Invoke(hit.point);
        }
    }

    public void GoLeft()
    {
        _direction = Vector2.left;
    }

    public void GoRight()
    {
        _direction = Vector2.right;
    }
}
