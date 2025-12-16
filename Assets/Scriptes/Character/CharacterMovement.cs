using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    [Header("Ground Detection")]
    [SerializeField] private Vector3 _groundCheckOffset;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private Transform _characterVisualModel;

    private float _moveInputX;
    private bool _isMoving;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _facingRight = true;

    private Rigidbody2D _rigidbody;
    private CharacterAnimations _animations;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _animations = GetComponent<CharacterAnimations>();
        if (_animations == null) _animations = GetComponentInChildren<CharacterAnimations>();

        if (_characterVisualModel == null)
        {
            var anim = GetComponentInChildren<Animator>();
            if (anim != null) _characterVisualModel = anim.transform;
            else _characterVisualModel = transform;
        }
    }

    private void Update()
    {
        CheckGround();

        _moveInputX = Input.GetAxis("Horizontal");
        _isMoving = Mathf.Abs(_moveInputX) > 0.1f;

        if (_animations != null)
        {
            _animations.IsMoving = _isMoving;
            _animations.IsFalling = !_isGrounded && _rigidbody.linearVelocity.y < -0.1f;
            _animations.IsJumping = _isJumping;
            _animations.IsGrounded = _isGrounded;
        }

        if (_rigidbody.linearVelocity.y < -0.1f)
        {
            _isJumping = false;
        }

        if (_moveInputX > 0 && !_facingRight) Flip();
        else if (_moveInputX < 0 && _facingRight) Flip();

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInputX * _speed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isJumping = true;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 scale = _characterVisualModel.localScale;
        scale.x *= -1;
        _characterVisualModel.localScale = scale;
    }

    private void CheckGround()
    {
        Vector3 checkPosition = transform.position + _groundCheckOffset;
        _isGrounded = Physics2D.OverlapCircle(checkPosition, _groundCheckRadius, groundMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _groundCheckOffset, _groundCheckRadius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            CoinManager.instance.AddCoin();
            Destroy(other.gameObject);
        }
    }
}