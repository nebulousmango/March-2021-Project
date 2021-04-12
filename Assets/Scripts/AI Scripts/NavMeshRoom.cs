using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshRoom : MonoBehaviour
{
    //Array to store Enemy Waypoints.
    Waypoint[] Wpt_Arr_Waypoints;
    //Array to store which Enemies belong in a NavMeshRoom.
    public Enemy[] Enemy_EnemiesInThisRoom;
    public Door dr_Enter;
    bool b_enteredRoom = false;

    private void Awake()
    {
        //Assigns Waypoint array values from all Waypoint objects in NavMeshRoom.
        Wpt_Arr_Waypoints = GetComponentsInChildren<Waypoint>();
        EnemyWaypointPopulation();
    }

    //Runs PopulateWaypointPositions function for all Enemies in NavMeshRoom.
    void EnemyWaypointPopulation()
    {
        foreach (Enemy enemy in Enemy_EnemiesInThisRoom)
        {
            enemy.PopulateWaypointPositions(Wpt_Arr_Waypoints);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInChildren<PlayerController>() && !b_enteredRoom)
        {
            dr_Enter.CloseDoor();
            b_enteredRoom = true;
        }
    }
}
