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

    //Sets trigger for Open Door animation.
    public void OpenDoor()
    {
        animator.SetTrigger("OpenDoor");
    }

    //Sets trigger for Close Door animation.
    public void CloseDoor()
    {
        animator.SetTrigger("CloseDoor");
    }
}
