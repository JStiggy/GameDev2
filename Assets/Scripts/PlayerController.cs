using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script should be on the character sprite

/* Keys config:
 * horizontal axis: 	horizontal motion
 * vertical axis: 		jumping
 * I:					accelerated mobility
 * U:					enhanced boost
 */
public class PlayerController : MonoBehaviour
{
    public float WalkSpeed;
    public float JumpBoost;
    public float RunSpeed;
    public float EnhancedBoostDuration;

    public float moveDirection;

    private bool Grounded = false;
    private bool BasicJump = true;  //Avoid input loss
    private bool EnhancedBoost = false;
    private float Duration = 0f;
    private bool EnhancedJump = false;
    private bool Glide = false;
    private bool EndGlide = false;

    void Update()
    {
        //Walking
        moveDirection = Input.GetAxisRaw("Horizontal");
        Grounded = Physics2D.OverlapCircle(transform.position - new Vector3(0, .5f, 0), .4f);
        Glide = Input.GetKey(KeyCode.U);
        EndGlide = Input.GetKeyUp(KeyCode.U);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * WalkSpeed * moveDirection);
        print(transform.right * WalkSpeed * moveDirection);
        if (Glide)
        {
            EndGlide = false;
            float vy = GetComponent<Rigidbody2D>().velocity.y;
            if (vy < 0f)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up *
                GetComponent<Rigidbody2D>().gravityScale * 0.9f * (-Physics2D.gravity.y));
            }
        }
        if(EndGlide)
        {
            Glide = false;
            GetComponent<Rigidbody2D>().AddForce(transform.up *
                GetComponent<Rigidbody2D>().gravityScale * 0.9f * Physics2D.gravity.y);
        }
    }
}
