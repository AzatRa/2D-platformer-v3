using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 90;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _timeForRegeneration = 5f;
    [SerializeField] private int _regenerationValue = 2;

    public event Action<int, int> OnChange;
    public event Action OnDied;

    private int _minHealth = 0;
    private float _timer;
    private bool _isTimerStarted;

    private void Start()
    {
        if (_health < _maxHealth)
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
            Regeneration();
        }
    }

    public void Change(int amount)
    {
        _health += amount;
        OnChange?.Invoke(_health, amount);

        if (_health <= _minHealth)
            Die();
    }

    private void Regeneration()
    {
        if (_health < _maxHealth && _timer >= _timeForRegeneration)
        {
            Change(_regenerationValue);
            _timer = 0;
        }

        if (_health >= _maxHealth)
        {
            _isTimerStarted = false;
            _health = _maxHealth;
        }
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
