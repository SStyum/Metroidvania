using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stopJumping;

    [Header("Ground Details")]
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOCircle;
    public bool grounded;

    [Header("Components")]
    private Rigidbody2D rb2D;
    private Animator myAnimator;
    

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        jumpTimeCounter = jumpTime;
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //what it means to be grounded
        grounded = Physics2D.OverlapCircle(groundcheck.position, radOCircle, whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }
        //use space to jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            stopJumping = false;
        }

        if (Input.GetButton("Jump") && !stopJumping && jumpTimeCounter > 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stopJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheck.position, radOCircle);
    }
}
