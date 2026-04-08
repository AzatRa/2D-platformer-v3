using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 90;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _timeForRegeneration = 5f;
    [SerializeField] private int _regenerationValue = 2;

    public event Action<int, int> OnChanged;
    public event Action OnDied;

    private int _minHealth = 0;
    private float _timer;
    private bool _isTimerStarted;
    private bool _isDied = false;

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

        if (_health == _minHealth && !_isDied)
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

    public void Heal(int heal)
    {
        if (heal < 0)
            return;

        Change(heal);
    }

    private void Change(int amount)
    {
        int oldHealth = _health;
        _health = Mathf.Clamp(_health + amount, _minHealth, _maxHealth);
        int currentChange = _health - oldHealth;
        OnChanged?.Invoke(_health, currentChange);
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
