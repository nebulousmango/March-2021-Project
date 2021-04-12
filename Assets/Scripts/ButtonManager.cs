using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //Variables to set scene names to be loaded. 
    public string S_LevelOne;
    public string S_LevelTwo;

    //Functions for loading levels. 
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(S_LevelOne);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(S_LevelTwo);
    }

    //Function for quitting game. 
    public void QuitGame()
    {
        Application.Quit();
    }
}