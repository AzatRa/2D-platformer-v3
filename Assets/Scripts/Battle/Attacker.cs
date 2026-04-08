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

    private void Awake()
    {
        _cooldownTimer = _cooldownTime;
    }

    private void Update()
    {
        if (_isAttacking)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeForAttack)
                Disable();
        }
        else
        {
            _cooldownTimer += Time.deltaTime;
        }
    }

    public void Enable()
    {
        if (_cooldownTimer < _cooldownTime || _isAttacking)
            return;

        StartAttack();
    }

    public void Disable()
    {
        OnAttackRelease?.Invoke();
        _isAttacking = false;
        _cooldownTimer = 0;
    }

    private void StartAttack()
    {
        _isAttacking = true;
        _timer = 0;
        OnAttack?.Invoke();
    }
}
