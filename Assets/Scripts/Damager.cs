using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage = -100;
    [SerializeField] private Attacker _attacker;

    private bool _isAttacking = false;

    private void Start()
    {
        _attacker.OnAttack += OnAttack;
        _attacker.OnAttackRelease += OnAttackRelease;
    }
    private void OnDestroy()
    {
        _attacker.OnAttack -= OnAttack;
        _attacker.OnAttackRelease -= OnAttackRelease;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking)
        {
            if (collision.TryGetComponent<Health>(out var health))
            {
                health.Change(_damage);
            }
        }
    }

    private void OnAttack()
    {
        _isAttacking = true;
    }

    private void OnAttackRelease()
    {
        _isAttacking = false;
    }
}
