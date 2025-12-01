using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { private get; set; }
    public bool IsFlying { private get; set; }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFlying", IsFlying);
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}