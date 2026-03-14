using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public event Action<Apple> CollidedWithPlayer;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            CollidedWithPlayer?.Invoke(this);
        }
    }
}
