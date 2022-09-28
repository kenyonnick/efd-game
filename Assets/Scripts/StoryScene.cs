using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScene : MonoBehaviour {
    public GameObject[] enabledByDefault;
    public GameObject[] disabledByDefault;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetSetup()
    {
        foreach (GameObject gObj in disabledByDefault)
            gObj.SetActive(false);

        foreach (GameObject gObj in enabledByDefault)
            gObj.SetActive(true);
    }
}
