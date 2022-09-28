using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowelBlock : MonoBehaviour {
    public float scaleShrinkPerHit;
    public float scaleGrowPerSecond;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x < 1.0f)
        {
            transform.localScale += new Vector3(scaleGrowPerSecond, scaleGrowPerSecond, scaleGrowPerSecond) * Time.deltaTime;
            //Debug.Log("local scale: " + transform.localScale);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (transform.localScale.x > scaleShrinkPerHit)
            {
                transform.localScale -= new Vector3(scaleShrinkPerHit, scaleShrinkPerHit, scaleShrinkPerHit);
            }
            //Debug.Log("HIT local scale: " + transform.localScale);
        }
    }
}
