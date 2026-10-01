using UnityEngine;

public class player : MonoBehaviour
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
        // Get input axes (-1 to 1)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize to prevent faster diagonal movement
        if (movement != Vector2.zero)
        {
            movement.Normalize();
        }
    }

    private void FixedUpdate()
    {
        // Apply movement using MovePosition for physics compatibility
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}