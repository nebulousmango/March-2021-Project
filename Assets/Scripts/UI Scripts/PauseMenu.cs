using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject Go_PauseMenuUI;
    bool GameIsPaused = false;

    private void Start()
    {
        Go_PauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Go_PauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0;
    }

    void Resume()
    {
        Go_PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1;
    }

    public void ChangePauseBool()
    {
        GameIsPaused = false;
    }
}
