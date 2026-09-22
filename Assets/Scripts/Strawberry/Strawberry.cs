using System;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [SerializeField] private int _healing = 100;

    public event Action<Strawberry> Collected;

    public int Healing => _healing;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
