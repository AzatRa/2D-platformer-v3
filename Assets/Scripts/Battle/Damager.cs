using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage = 100;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 0.15f;
    [SerializeField] private LayerMask _targetLayer;

    public event Action OnTakedDamage;

    private bool _isActive = false;

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

    private void Update()
    {
        if (_isActive)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _targetLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(_damage);
                OnTakedDamage?.Invoke();
                _isActive = false;
            }
        }
    }

    private void OnAttack()
    {
        _isActive = true;
    }

    private void OnAttackRelease()
    {
        _isActive = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
