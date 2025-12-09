using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { private get; set; }
    public bool IsFalling { private get; set; }
    public bool IsGrounded { private get; set; }
    public bool IsJumping { private get; set; }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFalling", IsFalling);
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetBool("IsJumping", IsJumping);
    }
}