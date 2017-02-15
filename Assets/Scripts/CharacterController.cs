using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script should be on the character sprite
public class CharacterController : MonoBehaviour 
{
	public float WalkSpeed;
	public float JumpBoost;
	public float RunSpeed;

	private bool Grounded = false;
	private bool BasicJump = false;	//Avoid input loss

	void Update()
	{	
		//Walking
		float HTranslate = Input.GetAxisRaw("Horizontal");
		if (Input.GetKey (KeyCode.I)) {
			//Replace the key for running if needed
			HTranslate *= Time.deltaTime * RunSpeed;
			//Reduce power for running
		}
		else 
		{
			HTranslate *= Time.deltaTime * WalkSpeed;
		}
		transform.Translate (HTranslate, 0f, 0f);
		//Play walking animation here

		//Jumping
		if (Input.GetAxisRaw ("Vertical") != 0 && Grounded) 
		{	
			Grounded = false;
			BasicJump = true;
			//Play jumping animation here

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
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		if(col.gameObject.tag == "Ground") 
		{
			Grounded = true;
		}
	}

}
