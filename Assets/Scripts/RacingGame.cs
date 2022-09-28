using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingGame : MonoBehaviour {
    public RaceCar raceCar;
    public RaceBlock[] raceBlocks;
    public int groupIndex = 0;
    public bool[] blocked;
    public bool runBlockCheck = false;
    public bool shouldBePaused = false;
    public Animator roadLinesAnim;

    [Space(10)]
    [Header("Stress Meter Properties")]
    public GameObject stressMeter;
    public float stressPercentage = 0.0001f;
    public float maxStressRate;
    public float stressRate;
    public float stressDecreaseRate;
    float unfill = 0;

    [Space(10)]
    [Header("Positioning")]
    public float moveRate = 16f;
    public float minX = 6f;
    public Vector3 endPosition;
    public Vector3 defaultPosition;

    [Space(10)]
    [Header("Game Management")]
    public NKGameManager nkgm;
    public bool gamePaused = false;

	// Use this for initialization
	void Start () {
        defaultPosition = this.transform.position;
        endPosition = defaultPosition;
        endPosition.x = minX;

        blocked = new bool[raceBlocks.Length];

        raceCar.rg = this;
        for(int i = 0; i < raceBlocks.Length; i++)
        {
            raceBlocks[i].rg = this;
            raceBlocks[i].Setup(i);
            blocked[i] = false;
            
        }

        SendNextLine();
	}

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            if (stressPercentage < 1.0f)
            {
                if(unfill > 0)
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

    void RunBlockCheck()
    {
        shouldBePaused = false;
        for (int i = 0; i < blocked.Length; i++)
        {
            if (blocked[i]) shouldBePaused = true;
        }
        if (shouldBePaused)
        {
            PauseGroup();
        }
        else
        {
            ResumeGroup();
        }

    }
	

    public void SendNextLine()
    {
        int shownCount = 0;
        for(int i = 0; i < 3; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            bool shouldShow = (rand >=0.5 ? true : false);
            // Check on the last one
            if(i == 2 && shownCount == 0) shouldShow = true;
            if (i == 2 && shownCount == 2) shouldShow = false;

            // Add the block if needed
            if (shouldShow)
            {
                shownCount++;
                RaceBlock block = raceBlocks[groupIndex + i];
                block.gameObject.SetActive(true);
                block.Reset(new Vector3(1.1f * (-1+i),3.5f,0));
                // make the first visible block the communicator
                if (shownCount == 1) block.communicator = true;
            }
            
        }
        unfill = stressDecreaseRate;
        Debug.Log("Unfill = " + unfill);
        // update the group index to the next group
        groupIndex = (groupIndex == 0 ? 3 : 0);
    }

    public void Blocked(int id)
    {
        blocked[id] = true;
        RunBlockCheck();
    }

    public void Unblocked(int id)
    {
        blocked[id] = false;
        RunBlockCheck();
    }

    public void PauseGroup()
    {
        roadLinesAnim.enabled = false;

        groupIndex = (groupIndex == 0 ? 3 : 0);
        for (int i = 0; i < 3; i++)
        {
            if (raceBlocks[i+groupIndex].gameObject.activeSelf)
            {
                raceBlocks[i+groupIndex].Pause();
                Debug.Log("Paused index " + i);
            }
        }
        groupIndex = (groupIndex == 0 ? 3 : 0);
    }

    public void ResumeGroup()
    {
        roadLinesAnim.enabled = true;

        groupIndex = (groupIndex == 0 ? 3 : 0);
        for (int i = 0; i < 3; i++)
        {
            if (raceBlocks[i+groupIndex].gameObject.activeSelf)
            {
                raceBlocks[i+groupIndex].Resume();
            }
        }
        groupIndex = (groupIndex == 0 ? 3 : 0);
    }

    public void UpdateStressMeter()
    {
        this.transform.position = defaultPosition - new Vector3(moveRate * stressPercentage, 0, 0);
        if(this.transform.position.x < minX)
        {
            this.transform.position = endPosition;
        }

        stressMeter.transform.localScale = new Vector3(1.0f, 1.0f - stressPercentage, 1.0f);
        stressMeter.transform.localPosition = new Vector3(0.0f, 2 * stressPercentage, -0.125f);
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
