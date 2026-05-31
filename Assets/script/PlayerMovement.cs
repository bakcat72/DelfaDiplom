using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    public int maxJumps = 2;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    [Header("Gravity")]
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int jumpsLeft;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckGround();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMove();
        ApplyBetterGravity();
    }

    void CheckGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // только что приземлились Ч восстанавливаем прыжки
        if (!wasGrounded && isGrounded)
            jumpsLeft = maxJumps;
    }

    void HandleMove()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(input * moveSpeed, rb.linearVelocity.y);

        // разворот спрайта
        if (input != 0)
            spriteRenderer.flipX = Mathf.Sign(input) < 0;
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsLeft--;
        }

        // срезаем прыжок при отпускании кнопки (дл€ "короткого" прыжка)
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
    }

    void ApplyBetterGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            // падение Ч гравитаци€ сильнее
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            // кнопку отпустили Ч тоже т€нет вниз быстрее
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}