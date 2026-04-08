using System.Runtime.CompilerServices;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Quaternion _leftRotation;
    private Quaternion _rightRotation;
    private int _angleForRotation = 180;


    private void Awake()
    {
        _leftRotation = Quaternion.Euler(0, _angleForRotation, 0);
        _rightRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Rotate(bool rotate)
    {
        if (rotate)
        {
            transform.rotation = _leftRotation;
        }
        else
        {
            transform.rotation = _rightRotation;
        }
    }
}