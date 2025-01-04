using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private SpriteRenderer spriteRenderer;
    private float lastCollisionCheckTime;
    private Animator animator;

    public float attackSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (Time.time - lastCollisionCheckTime > attackSpeed)
        {
            lastCollisionCheckTime = Time.time;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // Handle collision with enemy
                    collider.gameObject.GetComponent<Health>().DMG(5);
                    Debug.Log("Player collided with enemy");
                }
            }

            // Play animation every 2 seconds
            animator.SetFloat("Timer", 1f);
        }
        else
        {
            animator.SetFloat("Timer", 0f);
        }
    }
}

