using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour
{
    //Variable to set scene name to be loaded after Win. 
    public string S_SceneName;
    //Variable to set Win particle effect. 
    public GameObject Go_WinParticle;
    //Variable to set Player object. 
    public GameObject Go_PlayerObject;

    //Function for if player passes through Win object. 
    private void OnTriggerEnter(Collider other)
    {
        //Checks if player object contains the PlayerController script. 
        if (other.GetComponent<PlayerController>() == true)
        {
            //Starts Win coroutine.
            StartCoroutine(WinGame());
        }
    }

    //Function for Player's win sequence.
    IEnumerator WinGame()
    {
        //Plays win particle effect, waits for 5 seconds and then loads scene.
        GameObject currentParticle = GameObject.Instantiate(Go_WinParticle, Go_PlayerObject.transform);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(S_SceneName);
    }

}