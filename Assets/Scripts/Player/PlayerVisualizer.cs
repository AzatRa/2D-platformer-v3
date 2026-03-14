using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rotator))]
public class PlayerVisualizer : MonoBehaviour
{
    private const string IsRunning = "IsRunning";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";

    [SerializeField] private Player _player;
    [SerializeField] private Rotator _rotator;

    private Animator _animator;
    private bool _false = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rotator = GetComponent<Rotator>();
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

    public void ResetAnimation()
    {
        SwitchAnimationRun(_false);
        SwitchAnimationJump(_false);
        SwitchAnimationFall(_false);
    }

    public void GoLeft()
    {
        _rotator.Rotate(true);
    }

    public void GoRight()
    {
        _rotator.Rotate(false);
    }
}