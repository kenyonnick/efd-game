using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowelGame : MonoBehaviour {
    public GameObject paddle;
    public BowelBall ball;

    [Space(10)]
    [Header("Stress Meter Properties")]
    public GameObject stressMeter;
    public float stressPercentage = 0.0f;
    public float maxStressRate;
    public float stressRate;
    public float stressDecreaseRate;
    float unfill = 0;

    [Space(10)]
    [Header("Positioning")]
    public float moveRate = 16f;
    public float minX = 6;
    Vector3 endPosition;
    Vector3 defaultPosition;

    [Space(10)]
    [Header("Game Management")]
    public NKGameManager nkgm;
    public bool gamePaused = false;

    // Use this for initialization
    void Start () {
        defaultPosition = this.transform.position;
        endPosition = defaultPosition;
        endPosition.x = minX;

        ball.bg = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gamePaused)
        {
            if (stressPercentage < 1.0f)
            {
                if (unfill > 0)
                {
                    stressPercentage -= stressDecreaseRate * Time.deltaTime;
                    unfill -= Time.deltaTime;
                }
                if (stressPercentage < 0) stressPercentage = 0;
                stressPercentage += stressRate * Time.deltaTime;
                UpdateStressMeter();
            }
            else
            {
                nkgm.GameOver();
            }
        }
    }

    public void DecreaseStress()
    {
        unfill = stressDecreaseRate;
    }

    public void UpdateStressMeter()
    {
        this.transform.position = defaultPosition - new Vector3(moveRate * stressPercentage, 0, 0);
        if (this.transform.position.x < minX)
        {
            this.transform.position = endPosition;
        }
        stressMeter.transform.localScale = new Vector3(1.0f, 1.0f - stressPercentage, 1.0f);
        stressMeter.transform.localPosition = new Vector3(0.0f, -2 * stressPercentage, -0.125f);
    }

    public void SetStressRate(float rate)
    {
        stressRate = rate;
    }

    public void IncreaseStressRate(float inc)
    {
        stressRate += inc;
        if (stressRate > maxStressRate) stressRate = maxStressRate;
    }
}
