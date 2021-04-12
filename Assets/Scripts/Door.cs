using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Variable to access Door object's Animator.
    Animator animator;
    public bool b_startOpen;

    private void Start()
    {
        //Returns Door's Animator component.
        animator = GetComponentInChildren<Animator>();
        if (b_startOpen) OpenDoor();
        else CloseDoor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDoor();
        }
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
