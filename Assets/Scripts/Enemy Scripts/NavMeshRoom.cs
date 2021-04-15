using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshRoom : MonoBehaviour
{
    //Array to store which Enemies belong in a NavMeshRoom.
    public Enemy[] Enemy_EnemiesInThisRoom;
    //Variable to set entry door.
    public Door dr_Enter;
    //Variable to set exit door.
    public Door dr_Exit;
    //Array to store Enemy Waypoints.
    Waypoint[] Wpt_Arr_Waypoints;
    //Variable to store room's live Enemy count.
    int LiveEnemyCount;
    //Variable to store room's dead Enemy count.
    int DeadEnemyCount;
    //Bool to check whether Player has entered room.
    bool b_enteredRoom = false;

    private void Awake()
    {
        //Assigns Waypoint array values from all Waypoint objects in NavMeshRoom.
        Wpt_Arr_Waypoints = GetComponentsInChildren<Waypoint>();
        EnemyWaypointPopulation();
        //Returns number of live Enemies in room.
        LiveEnemyCount = Enemy_EnemiesInThisRoom.Length;
    }

    //Runs PopulateWaypointPositions function for all Enemies in NavMeshRoom.
    void EnemyWaypointPopulation()
    {
        foreach (Enemy enemy in Enemy_EnemiesInThisRoom)
        {
            enemy.PopulateWaypointPositions(Wpt_Arr_Waypoints);
        }
    }

    //Closes all doors after Player enters a room.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<PlayerController>() && !b_enteredRoom)
        {
            dr_Enter.CloseDoor();
            dr_Exit.CloseDoor();
            b_enteredRoom = true;
        }
    }

    //Used after each Enemy's death, increases count.
    public void ChangeDeadCount()
    {
        DeadEnemyCount++;
    }

    //Opens exit door if all Enemies are dead.
    public void CheckRoomEmpty()
    {
        if(DeadEnemyCount == LiveEnemyCount)
        {
            dr_Exit.OpenDoor();
        }
    }
}
