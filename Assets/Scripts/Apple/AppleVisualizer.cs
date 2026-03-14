using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AppleVisualizer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
