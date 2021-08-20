using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Variable to access Door object's Animator.
    Animator animator;
    //Public bool to set whether a Door is open or closed on Start.
    public bool b_startOpen;

    private void Start()
    {
        //Returns Door's Animator component.
        animator = GetComponentInChildren<Animator>();
        //Sets Doors to opened or closed based on bool value.
        if (b_startOpen) OpenDoor();
        else CloseDoor();
    }

    //Function for opening Door.
    public void OpenDoor()
    {
        //Sets trigger for Open Door animation.
        animator.SetTrigger("OpenDoor");
        //Plays the Door sound effect through the AudioManager script.
        FindObjectOfType<AudioManager>().PlaySound("Door");
    }

    //Function for closing Door.
    public void CloseDoor()
    {
        //Sets trigger for Close Door animation.
        animator.SetTrigger("CloseDoor");
    }
}
