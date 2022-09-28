using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimingNode : MonoBehaviour {
    public float waitTime;
    public bool hasTrigger = false;
    public KeyCode keyCode;
    bool triggered = false;
    public UnityEvent nextEvent;
    
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasTrigger) triggered = true;
        else if (Input.GetKeyDown(keyCode)) triggered = true;
        

        if (triggered)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                nextEvent.Invoke();
            }
        }
	}
}
