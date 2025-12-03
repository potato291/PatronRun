using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    [SerializeField] private Vector3 _groundCheckOffset;
    [SerializeField] private float _groundCheckRadius = 0.2f; 
    [SerializeField] private LayerMask groundMask;

    private float _moveInputX;
    private bool _isMoving;
    private bool _isGrounded;

    public CoinManager cm;

    private Rigidbody2D _rigidbody;
    private CharacterAnimations _animations;
    [SerializeField] private SpriteRenderer _characterSprite;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponentInChildren<CharacterAnimations>();
    }

    private void Update()
    {    
        CheckGround();

        _moveInputX = Input.GetAxis("Horizontal");
        _isMoving = _moveInputX != 0;

        _animations.IsMoving = _isMoving;
       
        _animations.IsFlying = !_isGrounded && IsFalling();

        if (_moveInputX > 0)
            _characterSprite.flipX = false;
        else if (_moveInputX < 0)
            _characterSprite.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                Jump();
                _animations.Jump();
            }
        }
    }

    private bool IsFalling()
    {
        return _rigidbody.linearVelocity.y < -0.1f;
    }

    private void CheckGround()
    {
        Vector3 checkPosition = transform.position + _groundCheckOffset;
        _isGrounded = Physics2D.OverlapCircle(checkPosition, _groundCheckRadius, groundMask);
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInputX * _speed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
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
            Destroy(other.gameObject);

            cm.AddCoin();
        }

    }
}