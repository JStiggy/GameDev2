using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    
    public float force_returned_co = 0.95f;
    
    private BlockedObject obj;

    void OnTriggerEnter2D(Collider2D coll)
    {
        obj = coll.gameObject.GetComponent<BlockedObject>();
        if(obj != null)
        {
            coll.gameObject.GetComponent<Rigidbody2D>().velocity *= -force_returned_co;
            if(obj.DestroyUponBlock)
            {
                Destroy(coll.gameObject);
                return;
            }
            if(obj.BreakShield)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

}
