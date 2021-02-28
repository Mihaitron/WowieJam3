using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool gamePaused;

    void Start()
    {
        gamePaused = false;
    }

    public void Action()
    {
        if (gamePaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
