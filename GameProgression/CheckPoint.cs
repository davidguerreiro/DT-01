using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public bool active = true;                                 // Whether this checkpoint is active or not. Not active checkpoint cannot be used.
    public bool currentCheckPoint;                             // Wheter this is the current checkpoint used when player is respawned.

    [Header("Enemy Groups")]
    public EnemyGroup[] enemyGroups;                    // Enemy groups controlled by this checkpoint.

    /// <summary>
    /// Reset enemy groups
    /// associated with this checkpoint.
    /// </summary>
    public void ResetEnemyGroups() {
        foreach ( EnemyGroup enemyGroup in enemyGroups ) {
            enemyGroup.ResetEnemyGroup();
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {
        // Debug.Log( "enter checkpoint" );
        if ( other.tag == "Player" && active && ! currentCheckPoint ) {
            ResetEnemyGroups();
            currentCheckPoint = true;
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit( Collider other ) {
        // Debug.Log( "leave checkpoint" );
        if ( other.tag == "Player" && active && currentCheckPoint ) {
            currentCheckPoint = false;
        }    
    }
}
