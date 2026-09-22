using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Apple> _apples = new List<Apple>();
    private List<Strawberry> _strawberries = new List<Strawberry>();

    public void Take(Apple apple)
    {
        _apples.Add(apple);
    }

    public void Take(Strawberry strawberry)
    {
        _strawberries.Add(strawberry);
    }
}
