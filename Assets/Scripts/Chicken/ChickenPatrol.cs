using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChickenPatrol : MonoBehaviour
{
    [SerializeField] private float _maxRunDistance = 3f;
    [SerializeField] private float _minRunDistance = 1f;

    private Vector2 _startPosition;
    private Vector2 _currentDirection;
    private bool _isNeedChangeDirection = false;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public Vector2 GetTargetPosition()
    {
        float minValue = -1;
        float maxValue = 1;

        if (_isNeedChangeDirection)
        {
            _currentDirection *= minValue;
            _isNeedChangeDirection = false;
        }
        else
        {
            Vector2 direction = new Vector2(Random.Range(minValue, maxValue), 0).normalized;
            _currentDirection = direction;
        }

        Vector2 position = _startPosition + _currentDirection * Random.Range(_minRunDistance, _maxRunDistance);
        return position;
    }

    public Vector2 ChangeDirection()
    {
        _isNeedChangeDirection = true;
        return GetTargetPosition();
    }
}
