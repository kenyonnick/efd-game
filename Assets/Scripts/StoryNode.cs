using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryNode : MonoBehaviour {
    public UnityEvent[] outcomes;
    public GameObject[] optionHighlight;
    int iter = 0;

	// Use this for initialization
	void Start () {
        if (optionHighlight.Length > 0)
        {
            optionHighlight[iter].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            outcomes[iter].Invoke();
        }

        // If there options, highlight the currently selected one
        if(optionHighlight.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                optionHighlight[iter].SetActive(false);
                iter--;
                if (iter < 0) iter = optionHighlight.Length - 1;
                optionHighlight[iter].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                optionHighlight[iter].SetActive(false);
                iter++;
                if (iter >= optionHighlight.Length) iter = 0;
                optionHighlight[iter].SetActive(true);
            }
            
        }
	}
}
