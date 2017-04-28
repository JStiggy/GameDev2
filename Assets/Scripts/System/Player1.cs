using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player1 : MonoBehaviour {

	public GameObject AIM;
	public GameObject blt;
	public float moveSpeed;
	private Vector3 moveDirection;
	public float turnSpeed;
    private float i = 0;
    private int j = 0;
    private int persis = 0;
    private bool pressed = false;
    bool enter = false;
    public GameObject forw;
    float originalz;
    LineRenderer lr;
    bool initiate = false;
    public float maxangle = 90;
    bool done;
    public float delaysecond=1;
    Rigidbody2D rb;
    // Use this for initialization

    

   
    void Awake () {
        initiate = false;
		moveDirection = Vector2.up;
        enter = false;
        lr = this.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        

    }
    private void Start()
    {
        done = false;
        lr.startWidth = 0;
    }

    // Update is called once per frame

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag=="Player")
        {
            enter = true;
            AIM = other.gameObject;

        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
            i = 0;
            AIM = null;
        }
        
    }
    
    void Update () {
        if (done) { AIM = null; rb.gravityScale = 1; forw.transform.localScale = new Vector3(0,0, 0); lr.startWidth = 0; }
        if (lr.startWidth > 0)
        {
            lr.startWidth -= 0.03f;
            if (lr.startWidth < 0) lr.startWidth = 0;
        }
        if (AIM == null) { enter = false; i = 0; forw.transform.localScale =new Vector3(0,0,0); return; }
        print(gameObject.transform.rotation.eulerAngles.z + "  ----  " + originalz);
        pressed = true;
        if (lr.startWidth > 0)
        {
            lr.startWidth -= 0.03f;
            if (lr.startWidth < 0) lr.startWidth = 0;
        }
        if (enter && pressed)i+=Time.deltaTime;
        if (i<= delaysecond) forw.transform.localScale = new Vector3(i/ Time.deltaTime * 0.5f / 30f, i/ Time.deltaTime * 0.5f / 30f, i/ Time.deltaTime * 0.5f / 30f);
        





        /*
		Vector3 direction;

		direction = new Vector3 (AIM.transform.position.x - transform.position.x, 
			AIM.transform.position.y - transform.position.y, 0f);
		Debug.Log (direction); 
		transform.rotation = Quaternion.Euler (new Vector3(Vector3.Angle(new Vector3(1f, 0f, 0f), direction), 0f, 
			Vector3.Angle(new Vector3(0f, 1f, 0f), direction)));
*/
        if (initiate == false)
        {
            originalz = transform.rotation.eulerAngles.z;
            print(gameObject.transform.rotation.eulerAngles.z + " dsdsds ----sdsd sdsd " + originalz);
            initiate = true;
        }
        
        Vector3 direction = AIM.transform.position - transform.position;
		direction.Normalize ();
        Vector3 offset = AIM.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        


        Vector3 currentPosition = transform.position;
		Vector3 moveToward = AIM.transform.position;
		moveDirection = moveToward - currentPosition;
		moveDirection.z = 0; 
		moveDirection.Normalize();
        Quaternion _lookRotation = Quaternion.LookRotation(moveDirection);
        Vector3 target = moveDirection * moveSpeed + currentPosition;                                                 
		
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
				Quaternion.Euler( 1, 1, targetAngle ), 
				turnSpeed * Time.deltaTime );
        if (Mathf.Abs(gameObject.transform.rotation.eulerAngles.z - originalz) >=maxangle)
        {
            done = true;
        }


            if (enter && pressed )  {
                if (i >= delaysecond) {
                    RaycastHit2D[] f = Physics2D.RaycastAll(transform.position, transform.right, 1000f);
                    for (int ix = 0; ix < f.Length; ix++)
                    {
                    print(f[ix].collider.name);
                    
                    if (f[ix].collider.tag == "Player")
                    {

                        Vector3 v = transform.right.normalized;
                        lr.SetPosition(0, forw.transform.position);

                        lr.SetPosition(1, f[ix].point);
                        i = 0;
                        lr.startWidth = 0.15f;
                        i = 0;
                        if (f[ix].collider.gameObject.GetComponent<PlayerController>().GetShieldOn() == false)
                        { GameManager.Manager.ReloadGame(); ; return; }
                        break;
                    }
                    else
                    {
                        Vector3 v = transform.right.normalized;
                        lr.SetPosition(0, forw.transform.position);

                        lr.SetPosition(1, f[ix].point);
                        i = 0;
                        lr.startWidth = 0.15f;
                        i = 0;
                        break;
                    }
                        

                    }
                }
                
                










        }




    }


}
