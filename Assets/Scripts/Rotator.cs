using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void Rotate(bool rotate)
    {
        int angleForRotation = 180;

        if (rotate)
        {
            transform.rotation = Quaternion.Euler(0, angleForRotation, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}