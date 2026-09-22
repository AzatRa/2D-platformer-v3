using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage = 100;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 0.15f;
    [SerializeField] private LayerMask _targetLayer;

    public event Action Attacked;

    public void Attack()
    {
        Attacked?.Invoke();
        DealDamage();
    }

    private void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            _attackPoint.position,
            _attackRadius,
            _targetLayer
        );

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
