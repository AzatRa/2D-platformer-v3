using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private int _healing = 10;

    public event Action<Apple> CollidedWithPlayer;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            CollidedWithPlayer?.Invoke(this);
        }

        if (collision.gameObject.TryGetComponent<Health>(out var health))
        {
            health.Change(_healing);
        }
    }
}
