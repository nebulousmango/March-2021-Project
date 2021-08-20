using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string S_LevelA;
    [SerializeField] private string S_LevelB;
    [SerializeField] private string S_LevelC;
    [SerializeField] private string S_LevelD;

    public void LoadLevelA()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelA);
    }

    public void LoadLevelB()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelB);
    }

    public void LoadLevelC()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelC);
    }
    public void LoadLevelD()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelD);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        FindObjectOfType<RetryMenu>().ChangePauseBool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}