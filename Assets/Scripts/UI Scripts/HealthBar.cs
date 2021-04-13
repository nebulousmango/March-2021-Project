using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    //Variable to access anchor child object's Transform.
    public Transform trans_Anchor;
    //Variable to access Player's Camera.
    Camera playerCam;
    //Variable to check for Player's Health UI.
    bool b_isUI = false;

    private void Start()
    {
        //Returns value for Playera's Camera object.
        playerCam = FindObjectOfType<PlayerController>().GetComponentInChildren<Camera>();
        //Checks if this object is part of the UI.
        if(GetComponentInParent<Canvas>())
        {
            b_isUI = true;
        }    
    }

    private void Update()
    {
        LookAtPlayer();        
    }

    //Sets Enemy healthbar to always look at Player.
    void LookAtPlayer()
    {
        //Only works for Healthbars that are not part of UI.
        if (!b_isUI)
        {
            transform.LookAt(playerCam.transform);
        }
    }

    //Change Anchor's X scale value.
    public void ScaleAnchor(float newScale)
    {
        trans_Anchor.localScale = new Vector3(newScale, trans_Anchor.localScale.y, trans_Anchor.localScale.z);
    }
}
