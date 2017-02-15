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

	private bool Grounded = false;
	private bool BasicJump = false;	//Avoid input loss
	private bool EnhancedBoost = false;
	private float Duration = 0f;
	private bool EnhancedJump = false;
	private bool Glide = false;
	private bool EndGlide = false;

	void Update()
	{	
		//Walking
		float HTranslate = Input.GetAxisRaw("Horizontal");
		if (Input.GetKey (KeyCode.I)) {
			//Replace the key for running if needed
			HTranslate *= Time.deltaTime * RunSpeed;
			//Reduce power while running
			//Play running animation here
		}
		else 
		{
			HTranslate *= Time.deltaTime * WalkSpeed;
			//Play walking animation here
		}
		transform.Translate (HTranslate, 0f, 0f);

		//Jumping
		if (Input.GetAxisRaw ("Vertical") != 0 && Grounded) 
		{	
			Grounded = false;
			if (!EnhancedBoost) 
			{
				EndGlide = true;
				BasicJump = true;
				//Play basic jumping animation here
			} 
			else 
			{
				EnhancedJump = true;
				//Play enhanced jumping animation here
			}

		}

		if (Input.GetKeyDown (KeyCode.U) && !EnhancedBoost) 
		{
			Duration = EnhancedBoostDuration;
			EnhancedBoost = true;
		}

		if (EnhancedBoost) 
		{
			BasicJump = false;
			Duration -= Time.deltaTime;
			if (Duration <= 0f) 
			{
				EnhancedBoost = false;
				EnhancedJump = false;
				Glide = false;
				EndGlide = true;
			}
			if (Input.GetAxisRaw ("Vertical") != 0 && !Grounded) 
			{
				Glide = true;
				EndGlide = false;
				//Play Glide Animation here
			} 
			else 
			{
				Glide = false;
				EndGlide = true;
			}
		}
		else 
		{
			Glide = false;
			EndGlide = true;
		}
	}

	void FixedUpdate()
	{
		if (BasicJump) 
		{
			BasicJump = false;
			Grounded = false;
			GetComponent<Rigidbody2D> ().AddForce (transform.up * JumpBoost);
		}
		if (EnhancedJump) 
		{
			EnhancedJump = false;
			Grounded = false;
			GetComponent<Rigidbody2D> ().AddForce (transform.up * JumpBoost * 1.3f);
		}
		if (Glide) 
		{
			EndGlide = false;
			float vy = GetComponent<Rigidbody2D> ().velocity.y;
			if (vy < 0f) {
				GetComponent<Rigidbody2D> ().AddForce (transform.up *
				GetComponent<Rigidbody2D> ().gravityScale * 0.9f * (-Physics2D.gravity.y));
			}
		} 
		if(EndGlide)
		{
			Glide = false;
			GetComponent<Rigidbody2D> ().AddForce (transform.up *
				GetComponent<Rigidbody2D> ().gravityScale * 0.9f * Physics2D.gravity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		if(col.gameObject.tag == "Ground") 
		{
			Grounded = true;
		}
	}
}