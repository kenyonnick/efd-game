using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowelPaddle : MonoBehaviour {
    Rigidbody2D rb;
    public Vector2 leftVelocity;
    public Vector2 rightVelocity;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.J))
        {
            rb.velocity = leftVelocity;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            rb.velocity = rightVelocity;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
