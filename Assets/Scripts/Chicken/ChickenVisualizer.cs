using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChickenVisualizer : MonoBehaviour
{
    private const string IsRunning = "IsRunning";
    private const string IsHitting = "IsHitting";

    [SerializeField] private Rotator _rotator;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchAnimation(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }

    public void SwitchAnimationHit()
    {
        _animator.SetTrigger(IsHitting);
    }

    public void GoLeft()
    {
        _rotator.Rotate(false);
        
    }

    public void GoRight()
    {
        _rotator.Rotate(true);
    }

    public AnimatorStateInfo GetAnimatorStateInfo()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }
}
