using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    [SerializeField]
    private float speed =  5f;
    [SerializeField]
    private float jumpForce = 15f;
    [SerializeField]
    private float groundCheckDistance = 0.65f;
    [SerializeField]
    private bool facingRight = true;

    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Horizontal movement
        if (horizontalInput > 0)
        {
            // move right
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            if (!facingRight) Flip();
        } else if (horizontalInput < 0)
        {
            // move left
            rb2D.velocity = new Vector2(speed * -1, rb2D.velocity.y);
            if (facingRight) Flip();
        } else if (horizontalInput == 0)
        {
            // stop moving in x direction
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        // Vertical movement
        if (verticalInput > 0)
        {
            // jump if on ground
            if (IsGrounded()) Jump();
        } else if (verticalInput < 0)
        {
            // crouch?
        }

        // Drawing a ray for debug purposes so we can see if the player is hitting the ground.
        Vector2 downward = transform.TransformDirection(Vector2.down) * groundCheckDistance;
        Debug.DrawRay(transform.position, downward, Color.yellow);

        
    }

    private void Flip()
    {
        facingRight = !facingRight;
        sprite.flipX = !sprite.flipX;
    }

    private bool IsGrounded()
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, mask);
        if (hit.collider)
        {
            Debug.Log("Hit something: " + hit.collider.gameObject.tag);
            return true;
        }
        return false;
    }
    
    private void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }
}
