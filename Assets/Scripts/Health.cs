using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _value = 90;
    [SerializeField] private int _maxValue = 100;
    [SerializeField] private float _timeForRegeneration = 5f;
    [SerializeField] private int _regeneratingValue = 2;

    private int _minValue = 0;
    private float _timer;
    private bool _isTimerStarted;
    private bool _isDied = false;

    public event Action<int, int> Changed;
    public event Action Died;

    private void Start()
    {
        if (_value < _maxValue)
        {
            _timer = 0;
            _isTimerStarted = true;
        }
    }

    private void Update()
    {
        if (_isTimerStarted)
        {
            _timer += Time.deltaTime;
            Regenerating();
        }

        if (_value == _minValue && !_isDied)
        {
            _isDied = true;
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        Change(-damage);
    }

    public void TakeValue(int heal)
    {
        if (heal < 0)
            return;

        Change(heal);
    }

    private void Change(int amount)
    {
        int oldValue = _value;
        _value = Mathf.Clamp(_value + amount, _minValue, _maxValue);
        int currentChange = _value - oldValue;
        Changed?.Invoke(_value, currentChange);
    }

    private void Regenerating()
    {
        if (_value < _maxValue && _timer >= _timeForRegeneration)
        {
            Change(_regeneratingValue);
            _timer = 0;
        }

        if (_value >= _maxValue)
        {
            _isTimerStarted = false;
            _value = _maxValue;
        }
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
