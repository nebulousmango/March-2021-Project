using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //Variables to set scene names to be loaded. 
    public string S_LevelOne;
    public string S_LevelTwo;
    public string S_LevelThree;

    //Functions for loading levels. 
    public void LoadLevelOne()
    {
        //Loads new scene.
        SceneManager.LoadScene(S_LevelOne);
        //Resumes game's time.
        Time.timeScale = 1;
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(S_LevelTwo);
        Time.timeScale = 1;
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene(S_LevelThree);
        Time.timeScale = 1;
    }

    //Function for quitting game. 
    public void QuitGame()
    {
        Application.Quit();
    }
}