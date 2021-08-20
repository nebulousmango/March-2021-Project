using UnityEngine;

public class RetryMenu : MonoBehaviour
{
    //Variables to set Retry and Player Health UI objects.
    public GameObject Go_RetryMenuUI;
    public GameObject Go_PlayerHealthbar;
    //Bool for whether Retry UI is open or not.
    bool GameIsPaused = false;

    private void Start()
    {
        //Sets the Retry UI to invisible when starting scene.
        Go_RetryMenuUI.SetActive(false);
    }
    
    private void Update()
    {
        //Opens and closes Retry UI if Escape key is pressed.
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
        //Pauses game's time.
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    //Undoes everything that the Retry function does.
    void Resume()
    {
        //Enables Player Health UI.
        Go_PlayerHealthbar.SetActive(true);
        //Disables Retry UI.
        Go_RetryMenuUI.SetActive(false);
        //Resumes game's time.
        GameIsPaused = false;
        Time.timeScale = 1;
    }

    public void ChangePauseBool()
    {
        GameIsPaused = false;
    }
}
