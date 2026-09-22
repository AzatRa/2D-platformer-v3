using UnityEngine;

public class StrawberryVisualizer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
