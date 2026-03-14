using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Apple> _apples = new List<Apple>();

    public void Take(Apple apple)
    {
        _apples.Add(apple);
    }
}
