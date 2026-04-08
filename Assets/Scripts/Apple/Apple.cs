using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private int _healing = 10;

    public event Action<Apple> OnCollected;

    public int Healing => _healing;

    public void Collect()
    {
        OnCollected?.Invoke(this);
    }
}
