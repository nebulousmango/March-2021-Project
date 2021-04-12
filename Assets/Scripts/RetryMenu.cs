using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryMenu : MonoBehaviour
{
    //Unity slots to set Retry and Player Health UI objects.
    public GameObject Go_RetryMenuUI;
    public GameObject Go_PlayerHealthbar;
    static bool GameIsPaused = false;

    private void Start()
    {
        //Sets the Retry UI to invisible when starting scene.
        Go_RetryMenuUI.SetActive(false);
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
                Retry();
            }
        }
    }
    //Retry function called during Player's Die function.
    public void Retry()
    {
        //Sets the Player Health UI to invisible.
        Go_PlayerHealthbar.SetActive(false);
        //Enables Retry UI.
        Go_RetryMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    void Resume()
    {
        Go_PlayerHealthbar.SetActive(true);
        Go_RetryMenuUI.SetActive(false);
        GameIsPaused = false;
    }
}
