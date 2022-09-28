using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NKGameManager : MonoBehaviour {
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject socialCanvas;
    public RacingGame rg;
    public BowelGame bg;

    bool gamePaused = false;

	// Use this for initialization
	void Start () {
        rg.nkgm = this;
        bg.nkgm = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}

    public void TogglePause()
    {
        gamePaused = !gamePaused;
        if (!gamePaused)
        {
            rg.gamePaused = true;
            bg.gamePaused = true;
            pausePanel.SetActive(true);
            socialCanvas.SetActive(false);
        }
        else
        {
            rg.gamePaused = false;
            bg.gamePaused = false;
            pausePanel.SetActive(false);
            socialCanvas.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        socialCanvas.SetActive(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
