using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lấy input từ bàn phím (A/D hoặc mũi tên trái/phải)
        float moveX = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(moveX, 0f);
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật theo hướng nhập vào
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}
