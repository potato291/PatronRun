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
        _animator = GetComponent<Animator>();

        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        if (_animator == null)
            Debug.LogError("CRITICAL ERROR: Не могу найти Animator ни на собаке, ни внутри неё!");
    }

    private void FixedUpdate()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsMoving", IsMoving);
            _animator.SetBool("IsFalling", IsFalling);
            _animator.SetBool("IsGrounded", IsGrounded);
            _animator.SetBool("IsJumping", IsJumping);
        }
    }
}