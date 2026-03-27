using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _timeForAttack = 2f;
    [SerializeField] private float _cooldownTime = 2f;
    
    public event Action OnAttack;
    public event Action OnAttackRelease;

    private float _timer;
    private float _cooldownTimer;
    private bool _isAttacking = false;

    private void Update()
    {
        if (_isAttacking)
        {
            _timer += Time.deltaTime;
            GetDamage();
        }
        else
        {
            _cooldownTimer += Time.deltaTime;
        }
    }

    public void Go()
    {
        _isAttacking = true;
    }

    public void Stop()
    {
        OnAttackRelease?.Invoke();
        _isAttacking = false;
        _timer = 0;
    }

    private void GetDamage()
    {
        if (_timer < _timeForAttack && _cooldownTimer >= _cooldownTime)
        {
            OnAttack?.Invoke();
        }
        else
        {
            Stop();
        }
    }
}
