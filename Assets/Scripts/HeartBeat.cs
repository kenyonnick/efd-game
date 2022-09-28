using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour {
    public float baseBPM = 80;
    public float maxBPM = 100;
    float timer = 0;
    public AudioClip heartBeatSound;
    AudioSource src;
    RacingGame rg;

	// Use this for initialization
	void Start () {
        src = GetComponent<AudioSource>();
        rg = GetComponent<RacingGame>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            src.PlayOneShot(heartBeatSound);
            // Get stressPercentage from RacingGame
            float perc = rg.stressPercentage;
            float bpm = baseBPM + (maxBPM - baseBPM) * perc;
            // set timer according to BPM
            timer = 60 / bpm;
        }
	}
}
