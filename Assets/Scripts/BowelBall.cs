using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowelBall : MonoBehaviour {
    Rigidbody2D rb;
    public float ballSpeed;
    public Vector2 velocity;
    Vector3 defaultPosition;
    public BowelGame bg;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,-ballSpeed);
        velocity = rb.velocity;
        defaultPosition = transform.localPosition;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Reset
        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetBall();
        }
        else if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.L))
                rb.velocity = velocity;
        }
        else if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.L))
            velocity = rb.velocity;
        else
        {
            
            rb.velocity = new Vector2(0, 0);
        }
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Vector3 dif = Vector3.Normalize(this.transform.position - collision.gameObject.transform.position);
            rb.velocity = new Vector2(dif.x * ballSpeed, -velocity.y);
            velocity = rb.velocity;
            bg.DecreaseStress();
        }

        if(collision.gameObject.CompareTag("Paddle")
            || collision.gameObject.CompareTag("Bottom Wall"))
        {
            Vector3 dif = Vector3.Normalize(this.transform.position - collision.gameObject.transform.position);
            rb.velocity = new Vector2(dif.x * ballSpeed, -velocity.y);
            velocity = rb.velocity;
        }

        else if(collision.gameObject.CompareTag("Side Wall"))
        {
            rb.velocity = new Vector2(-velocity.x, velocity.y);
            velocity = rb.velocity;
        }

        else if (collision.gameObject.CompareTag("Top Wall"))
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.localPosition = defaultPosition;
        rb.velocity = new Vector2(0, -ballSpeed);
        velocity = rb.velocity;
    }
}
