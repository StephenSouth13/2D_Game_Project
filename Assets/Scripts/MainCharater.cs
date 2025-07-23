using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacter : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
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

    private PlayerControls controls;

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

        // Nhảy
        controls.Player.Jump.performed += ctx =>
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (debugMode) Debug.Log("Nhảy!");
            }
        };
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
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);

        if (debugMode)
        {
            Debug.Log("Vận tốc hiện tại: " + rb.linearVelocity);
        }
    }
}