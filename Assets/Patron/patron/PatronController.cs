using UnityEngine;

public class DogController : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform model;   // сам об'єкт зі спрайтом (Patron)
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool facingRight = true;
    private bool isJumping = false;

    void Update()
    {
        // --- Рух по горизонталі ---
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * runSpeed, rb.linearVelocity.y);

        // --- Встановлюємо параметр руху для анімацій ---
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // --- Розворот (flip) ---
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        // --- Перевірка чи на землі ---
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // --- Стрибок ---
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        // --- Коли стрибок закінчується і пес падає на землю ---
        if (isGrounded && isJumping && rb.linearVelocity.y <= 0.1f)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void FixedUpdate()
    {
        // Застосовуємо стрибок тут, у фізичному апдейті
        if (isJumping && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = model.localScale;
        scale.x *= -1;
        model.localScale = scale;
    }
}
