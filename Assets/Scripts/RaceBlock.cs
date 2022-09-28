using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBlock : MonoBehaviour {
    public RacingGame rg;
    Rigidbody2D rb;
    public float blockSpeed;
    public Vector2 velocity;
    public Vector3 startPos;

    public bool communicator;
    int id;
    public bool blocked = false;

	// Use this for initialization
	void Start () {
        
    }

    public void Setup(int newId)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -blockSpeed);
        velocity = rb.velocity;
        startPos = transform.position;
        id = newId;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.O)) && !blocked)
        {
            rb.velocity = new Vector2(0,-blockSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bottom Wall"))
        {
            // Send next line in Racing Game
            if (communicator) rg.SendNextLine();
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Car"))
        {
            if (collision.gameObject.transform.position.y < this.transform.position.y)
            {
                Debug.Log("Car collided with " + name);
                rg.Blocked(id);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            rg.Unblocked(id);
        }
    }

    public void Resume()
    {
        blocked = false;
    }

    public void Pause()
    {
        blocked = true;
    }

    public void Reset(Vector3 newPos)
    {
        rb.velocity = new Vector2(0, -blockSpeed);
        velocity = rb.velocity;
        transform.localPosition = newPos;
    }
}
