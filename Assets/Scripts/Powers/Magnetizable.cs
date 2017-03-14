using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Magnetizable : MonoBehaviour {

    public float magneticRadius = 10f;
    public bool magnetized = true;
    public float force = 200f;

    Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
		if(magnetized)
        {
            GameObject parent = null;
            float distance = float.MaxValue;
            Vector3 movementPosition = this.transform.position;
            foreach (Collider2D col in Physics2D.OverlapCircleAll(this.transform.position, magneticRadius, 1<<9))
            {
                if (distance > Vector3.Distance(transform.position, col.transform.position))
                {
                    distance = Vector3.Distance(transform.position, col.transform.position);
                    movementPosition = col.transform.position;
                    parent = col.gameObject;
                }
            }
            if (movementPosition != this.transform.position)
            {
                transform.parent = parent.transform;
                rb.AddForce(((movementPosition - transform.position).normalized) / distance * Time.deltaTime * force, ForceMode2D.Impulse);
            }
        }
        else
        {
            transform.parent = null;
        }
	}
}
