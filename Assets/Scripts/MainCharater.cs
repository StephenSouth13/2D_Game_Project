using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool debugMode = true;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Di chuyển ngang: A/D hoặc Mũi tên trái/phải
        float moveX = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(moveX, 0f);

        // Kiểm tra tiếp đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Nhảy: W / ↑ / Space
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (debugMode) Debug.Log("Nhảy!");
        }

        if (debugMode && moveX != 0)
        {
            Debug.Log("Đang di chuyển: " + (moveX > 0 ? "Phải" : "Trái"));
        }
    }

    void FixedUpdate()
    {
        // Di chuyển ngang
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);

        if (debugMode)
        {
            Debug.Log("Vận tốc hiện tại: " + rb.linearVelocity);
        }
    }
}
