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
    public PlayerController player;

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
        player.Win();
        //Plays Win particle effect.
        GameObject currentParticle = GameObject.Instantiate(Go_WinParticle, Go_PlayerObject.transform);
        //Plays the Win sound effect through the AudioManager script.
        FindObjectOfType<AudioManager>().PlaySound("Win");
        //Loads scene after waiting.
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(S_SceneName);
    }

}