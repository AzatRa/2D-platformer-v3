using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public event Action<Apple> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
