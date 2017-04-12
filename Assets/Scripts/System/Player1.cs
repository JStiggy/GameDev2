using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {

	public GameObject AIM;
	public GameObject blt;
	public float moveSpeed;
	private Vector3 moveDirection;
	public float turnSpeed;
    private int i = 0;
    private int j = 0;
    private int persis = 0;
    private bool pressed = false;
    bool enter = false;
    public GameObject forw;
    LineRenderer lr;
    // Use this for initialization

    

   
    void Awake () {
		moveDirection = Vector2.up;
        enter = false;
        lr = this.GetComponent<LineRenderer>();
        AIM=GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag=="Player")
        {
            enter = true;
            
           
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
            i = 0;
        }
        
    }
    
    void Update () {
        pressed = true;
        if (lr.startWidth > 0)
        {
            lr.startWidth -= 0.03f;
            if (lr.startWidth < 0) lr.startWidth = 0;
        }
        if (enter && pressed)i++;
        if (i<=60)forw.transform.localScale = new Vector3(i * 0.5f / 60f, i * 0.5f / 60f, i * 0.5f / 60f);
        if (i > 60) forw.transform.localScale = new Vector3(60f * 0.5f / 60f, 60f * 0.5f / 60f, 60f * 0.5f / 60f);
        




        /*
		Vector3 direction;

		direction = new Vector3 (AIM.transform.position.x - transform.position.x, 
			AIM.transform.position.y - transform.position.y, 0f);
		Debug.Log (direction); 
		transform.rotation = Quaternion.Euler (new Vector3(Vector3.Angle(new Vector3(1f, 0f, 0f), direction), 0f, 
			Vector3.Angle(new Vector3(0f, 1f, 0f), direction)));
*/
        if (AIM == null) { enter = false; return; }
        Vector3 direction = AIM.transform.position - transform.position;
		direction.Normalize ();
        Vector3 offset = AIM.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);


        Vector3 currentPosition = transform.position;
		Vector3 moveToward = AIM.transform.position;
		moveDirection = moveToward - currentPosition;
		moveDirection.z = 0; 
		moveDirection.Normalize();
        Quaternion _lookRotation = Quaternion.LookRotation(moveDirection);
        Vector3 target = moveDirection * moveSpeed + currentPosition;                                                 
		transform.position = Vector3.Lerp( currentPosition, currentPosition, Time.deltaTime );
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
				Quaternion.Euler( 1, 1, targetAngle ), 
				turnSpeed * Time.deltaTime );

		
            if (enter && pressed )  {
                if (i >= 60) {
                    RaycastHit2D[] f = Physics2D.RaycastAll(transform.position, transform.right, 1000f);
                    for (int ix = 0; ix < f.Length; ix++)
                    {
                        if (f[ix].collider.tag == "Player") {

                            Vector3 v = transform.right.normalized;
                            lr.SetPosition(0, forw.transform.position );
                            
                            lr.SetPosition(1, f[ix].point);
                            i = 0;
                        lr.startWidth = 0.15f;
                        i = 0;
                        
                        Destroy(f[ix].collider.gameObject);
                    }
                        

                    }
                }
                
                










        }




    }


}
