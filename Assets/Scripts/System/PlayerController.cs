using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CoolDown
{
	public bool Magnitize = false;
	public bool Vision = false;
	public bool Glide = false;
	public bool Boost = false;
	public bool Shield = false;
    public bool jump = false;
}

public class PlayerController : MonoBehaviour
{
	public CoolDown AbilityCooldown;

    public float WalkSpeed;
    public float JumpBoost;
    public float EnhancedJumpCo;
    public float RunSpeed;
    public float BoostDuration;
	public GameManager data;

    public GameObject shield;

    public GameObject UI;
    

    [HideInInspector]
    int facingDirection = 1;
    public float moveDirection;

    public bool control = true;
    public bool activate = false;

    private Animator anim;
    private Rigidbody2D rb;
	public float FatalYVelocity = 0f;

    private bool Grounded = false;

    /*Basic Jump*/
    private bool BasicJump = false;
    private bool EnhancedBoost = false;
    private float Duration = 0f;

    /*Enhanced Jump*/
    private bool EnhancedJump = false;

    /*Glide*/
    private bool Glide = false;
    private bool EndGlide = false;

    /*Shield*/
    private bool shield_on = false;
    Magnetizable magObj = null;

	/*Boost*/
	private float boost = 0f;

    /*UI*/
    private GameObject eyes;
    private GameObject magnet;
    private GameObject momentum;
    private GameObject rocket;
    private GameObject barrier;

    private float alpha;

    public bool GetShieldOn()
	{
		return shield_on;
	}

    void Start()
    {
        data = GameManager.Manager;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();

        UI = GameObject.Find("UI");
        Transform[] ts = UI.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if(t.gameObject.name == "Eyes")
            {
                eyes = t.gameObject;
            }
            else if(t.gameObject.name == "Magnet")
            {
                magnet = t.gameObject;
            }
            else if(t.gameObject.name == "Momentum")
            {
                momentum = t.gameObject;
            }
            else if(t.gameObject.name == "Rocket")
            {
                rocket = t.gameObject;
            }
            else if(t.gameObject.name == "Barrier")
            {
                barrier = t.gameObject;
            }
        }
        Color temp = barrier.GetComponent<Image>().color;
        alpha = temp.a;
    }

    void Update()
    {

        AbilityCooldown.Magnitize = (((long)1 << 1) & GameManager.Manager.playerData.saveFlags) > 0;
        AbilityCooldown.Vision = (((long)1 << 33) & GameManager.Manager.playerData.saveFlags) > 0;
        AbilityCooldown.Glide = (((long)1 << 34) & GameManager.Manager.playerData.saveFlags) > 0;
        AbilityCooldown.jump = (((long)1 << 37) & GameManager.Manager.playerData.saveFlags) > 0;
        AbilityCooldown.Shield = (((long)1 << 36) & GameManager.Manager.playerData.saveFlags) > 0;
        AbilityCooldown.Boost = (((long)1 << 37) & GameManager.Manager.playerData.saveFlags) > 0;

        activate = false;
        if (!control)
        {
            anim.SetFloat("xVel", 0);
            anim.SetBool("grounded", Grounded);
            return;
        }

        //Walking
        moveDirection = Input.GetAxisRaw("Horizontal");
        if (moveDirection != 0)
        {
            facingDirection = (int)moveDirection;
        }

		/*
		//Grounded test
		Collider2D ground;
        ground = Physics2D.OverlapCircle(transform.position - new Vector3(0, .8f, 0), .4f, ~(1 << 8));

		if (ground)
			Grounded = (ground.tag == "Ground");
		else
			Grounded = false;
		*/


		//Gliding status test
        EndGlide = Input.GetKeyUp(KeyCode.U) || Grounded;
        Glide = Input.GetKey(KeyCode.U) && !Grounded && AbilityCooldown.Glide;

        anim.SetBool("grounded", Grounded);
        anim.SetBool("gliding", Glide);
        anim.SetFloat("xVel", moveDirection);


		if (data.playerData.energyReserve <= 0f) 
		{
			Glide = false;
			EndGlide = true;
			shield_on = false;
			EnhancedJump = false;
		}
        
        if(shield_on)
        {
            shield.SetActive(true);
			data.playerData.energyReserve -= 2f * Time.deltaTime;
            Color temp = barrier.GetComponent<Image>().color;
            temp.a += 30;
            barrier.GetComponent<Image>().color = temp;
        }
        else
        {
            shield.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.K) && AbilityCooldown.Shield)
        {
            shield_on = !shield_on;
            Color temp = barrier.GetComponent<Image>().color;
            temp.a = alpha;
            barrier.GetComponent<Image>().color = temp;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Manager.FadeOut(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.E) )
        {
            activate = true;
        }

        if (Input.GetKeyDown(KeyCode.I) && magObj == null && AbilityCooldown.Magnitize)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position + transform.right * facingDirection * .5f, .4f, 1 << 12);
            if (col != null)
            {
                magObj = col.gameObject.GetComponent<Magnetizable>();
                magObj.magnetized = true;
                Color temp = magnet.GetComponent<Image>().color;
                temp.a += 30;
                magnet.GetComponent<Image>().color = temp;
            }
        }
        else if (Input.GetKeyDown(KeyCode.I) && magObj != null && AbilityCooldown.Magnitize)
        {
            magObj.magnetized = false;
            magObj = null;
            Color temp = magnet.GetComponent<Image>().color;
            temp.a = alpha;
            magnet.GetComponent<Image>().color = temp;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !BasicJump && !EnhancedJump && AbilityCooldown.jump)
        {
            if (Grounded)
            {
				if (boost > 0f)
					BasicJump = true;
				else 
				{
					EnhancedJump = true;
				}
            }
        }

		if(Input.GetKeyDown(KeyCode.J) && boost <= 0f && AbilityCooldown.Boost)
        {
			boost = BoostDuration;
        }

		if(boost > 0f)
			boost -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!control)
        {
            return;
        }

        rb.velocity = new Vector3(WalkSpeed * moveDirection, rb.velocity.y, 0);
        if (Glide)
        {
            EndGlide = false;
            float vy = rb.velocity.y;
            if (vy < 0f)
            {
                rb.AddForce(transform.up * rb.gravityScale * 0.9f * (-Physics2D.gravity.y));
            }
            Color temp = rocket.GetComponentInChildren<Image>().color;
            temp.a += 30;
            rocket.GetComponent<Image>().color = temp;
        }
        if(EndGlide)
        {
            Glide = false;
            rb.AddForce(transform.up * rb.gravityScale * 0.9f * Physics2D.gravity.y);
            Color temp = rocket.GetComponentInChildren<Image>().color;
            temp.a = alpha;
            rocket.GetComponent<Image>().color = temp;
        }
        if(BasicJump)
        {
            rb.AddForce(new Vector2(0f, JumpBoost));
            BasicJump = false;
        }
        if(EnhancedJump)
        {
            rb.AddForce(new Vector2(0f, EnhancedJumpCo * JumpBoost));
            EnhancedJump = false;
        }
    }

    //For currect velocity detection
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Grounded = true;
            if (rb.velocity.y >= FatalYVelocity)
            {
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground") {
			Grounded = true;
			if (rb.velocity.y >= FatalYVelocity) 
			{

			}
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			Grounded = false;
		}
	}
}
