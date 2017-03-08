﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float WalkSpeed;
    public float JumpBoost;
    public float RunSpeed;
    public float EnhancedBoostDuration;
    int facingDirection = 1;
    public float moveDirection;

    public bool control = true;
    public bool activate = false;

    private Animator anim;
    private Rigidbody2D rb;

    private bool Grounded = false;
    private bool BasicJump = true;  //Avoid input loss
    private bool EnhancedBoost = false;
    private float Duration = 0f;
    private bool EnhancedJump = false;
    private bool Glide = false;
    private bool EndGlide = false;

    Magnetizable magObj = null;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        activate = false;
        if (!control)
        {
            return;
        }
        //Walking
        moveDirection = Input.GetAxisRaw("Horizontal");
        if (moveDirection != 0)
        {
            facingDirection = (int)moveDirection;
        }
        Grounded = Physics2D.OverlapCircle(transform.position - new Vector3(0, .8f, 0), .4f, ~(1 << 8));

        EndGlide = Input.GetKeyUp(KeyCode.U);
        Glide = Input.GetKey(KeyCode.U);

        anim.SetBool("grounded", Grounded);
        anim.SetBool("gliding", Glide);
        anim.SetFloat("xVel", moveDirection);

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Manager.FadeOut(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.E) )
        {
            print("Pressed");
            activate = true;
        }

        if (Input.GetKeyDown(KeyCode.I) && magObj == null)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position + transform.right * facingDirection * .5f, .4f, 1 << 12);
            if (col != null)
            {
                magObj = col.gameObject.GetComponent<Magnetizable>();
                magObj.magnetized = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.I) && magObj != null)
        {
            magObj.magnetized = false;
            magObj = null;
        }
    }

    void FixedUpdate()
    {
        if (!control)
        {
            return;
        }
        rb.velocity = new Vector3(1 * WalkSpeed * moveDirection, rb.velocity.y, 0);
        if (Glide)
        {
            EndGlide = false;
            float vy = rb.velocity.y;
            if (vy < 0f)
            {
                rb.AddForce(transform.up * rb.gravityScale * 0.9f * (-Physics2D.gravity.y));
            }
        }
        if(EndGlide)
        {
            Glide = false;
            rb.AddForce(transform.up * rb.gravityScale * 0.9f * Physics2D.gravity.y);
        }
    }
}