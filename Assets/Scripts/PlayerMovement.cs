using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX;
    private enum MovementState { idle, running, jumping, falling};
    [SerializeField] private float MoveSpeed = 7f;
    [SerializeField] private float JumpForce = 14f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(MoveSpeed*dirX, rb.velocity.y); //left right movement

        if (Input.GetButtonDown("Jump") && isGrounded()) { //jump
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        UpdateAnimationState();
        
    }

    private void UpdateAnimationState() 
    {
        MovementState state;
        
        //running animation
        if (dirX > 0f) {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) {
            state = MovementState.running;

            sprite.flipX = true;
        }
        else {
            state = MovementState.idle;

        }
        //jumping and falling animation
        if (rb.velocity.y > .01f) { 
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.01f) {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
