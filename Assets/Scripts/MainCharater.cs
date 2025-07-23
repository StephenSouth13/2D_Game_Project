using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MainCharacter : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runMultiplier = 1.8f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Debug")]
    public bool debugMode = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerControls controls;
    private Vector2 movement;
    private bool isGrounded;
    private bool isRunning = false;

    [Header("Bắn đạn")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    void Awake()
    {
        controls = new PlayerControls();

        // Di chuyển ngang
        controls.Player.Move.performed += ctx =>
        {
            movement = ctx.ReadValue<Vector2>();
        };
        controls.Player.Move.canceled += ctx =>
        {
            movement = Vector2.zero;
        };

        // Nhảy khi đang trên mặt đất
        controls.Player.Jump.performed += ctx =>
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (debugMode) Debug.Log("Nhảy!");
            }
        };

        // Chạy nhanh khi giữ Shift
        controls.Player.Run.performed += ctx => isRunning = true;
        controls.Player.Run.canceled += ctx => isRunning = false;

        controls.Player.Fire.performed += ctx => Shoot();
    }

    void OnEnable()
    {
        if (controls == null) controls = new PlayerControls();
        controls.Enable();
    }

    void OnDisable()
    {
        if (controls != null) controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Kiểm tra groundCheck đã được gán chưa
        if (groundCheck == null)
        {
            Debug.LogError("⚠️ Thiếu groundCheck! Gán điểm kiểm tra tiếp đất trong Inspector.");
        }
    }

    void Update()
    {
        // Kiểm tra tiếp đất
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // Quay đầu theo chiều di chuyển
        if (spriteRenderer != null && movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }

        if (debugMode && movement.x != 0)
        {
            Debug.Log("Đang di chuyển: " + (movement.x > 0 ? "Phải" : "Trái"));
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = isRunning ? moveSpeed * runMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(movement.x * currentSpeed, rb.linearVelocity.y);
        rb.linearVelocity = new Vector2(movement.x * currentSpeed, movement.y * currentSpeed);

        if (debugMode)
        {
            Debug.Log("Vận tốc hiện tại: " + rb.linearVelocity);
        }
    }
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (debugMode) Debug.Log("Bắn đạn!");
        }
    }
}