using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
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
    private Vector2 movement;
    private bool isGrounded;
    private bool isRunning = false;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        // Di chuyển
        controls.Player.Move.performed += ctx =>
        {
            movement = ctx.ReadValue<Vector2>();
        };
        controls.Player.Move.canceled += ctx =>
        {
            movement = Vector2.zero;
        };

        // Nhảy
        controls.Player.Jump.performed += ctx =>
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (debugMode) Debug.Log("Nhảy!");
            }
        };

        // Chạy nhanh (giữ Shift)
        controls.Player.Run.performed += ctx => isRunning = true;
        controls.Player.Run.canceled += ctx => isRunning = false;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (debugMode && movement.x != 0)
        {
            Debug.Log("Đang di chuyển: " + (movement.x > 0 ? "Phải" : "Trái"));
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = isRunning ? moveSpeed * runMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(movement.x * currentSpeed, rb.linearVelocity.y);

        if (debugMode)
        {
            Debug.Log("Vận tốc hiện tại: " + rb.linearVelocity);
        }
    }
}