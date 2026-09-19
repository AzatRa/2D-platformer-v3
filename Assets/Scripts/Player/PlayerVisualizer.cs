using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisualizer : MonoBehaviour
{
    private const string IsRunning = "IsRunning";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";
    private const string IsHitting = "IsHitting";
    private const string IsAttacking = "IsAttacking";

    [SerializeField] private Player _player;
    [SerializeField] private Rotator _rotator;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchAnimationRun(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }

    public void SwitchAnimationJump(bool isJumping)
    {
        _animator.SetBool(IsJumping, isJumping);
    }

    public void SwitchAnimationFall(bool isFalling)
    {
        _animator.SetBool(IsFalling, isFalling);
    }

    public void SwitchAnimationHit()
    {
        _animator.SetTrigger(IsHitting);
    }

    public void SwitchAnimationAttack()
    {
        _animator.SetTrigger(IsAttacking);
    }

    public void ResetAnimation()
    {
        SwitchAnimationRun(false);
        SwitchAnimationJump(false);
        SwitchAnimationFall(false);
    }

    public void GoLeft()
    {
        _rotator.Rotate(true);
    }

    public void GoRight()
    {
        _rotator.Rotate(false);
    }

    public AnimatorStateInfo GetAnimatorStateInfo()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }
}