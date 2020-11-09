using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupsProgression : LocalProgressionData {
    
    [SerializeField]
    public List<EnemyGroup> enemyGroups;                              // Enemy groups reference.

    /// <summary>
    /// Add enemy group to the
    /// enemy group list.
    /// </summary>
    /// <param name="enemyGroup">EnemyGroup - enemy group instance reference.</parma>
    public void RegisterEnemyGroup( EnemyGroup enemyGroup ) {
        if ( ! CheckIfRegistered( enemyGroup.id ) ) {
            enemyGroups.Add( enemyGroup );
        }
    }

    /// <summary>
    /// Check if enemy group exists.
    /// </summary>
    /// <param name="groupID">int - enemy group id</param>
    /// <returns>bool</returns>
    public bool CheckIfRegistered( int groupID ) {
        bool found = false;

        foreach ( EnemyGroup enemyGroup in enemyGroups ) {
            if ( enemyGroup.id == groupID ) {
                found = true;
                break;
            }
        }

        return found;
    }

    /// <summary>
    /// Get enemy group
    /// from the list.
    /// </summary>
    /// <param name="groupID">int - enemy group id to get</param>
    /// <returns>EnemyGroup</returns>
    public EnemyGroup GetEnemyGroup( int groupID ) {
        EnemyGroup reference = null;

        foreach ( EnemyGroup enemyGroup in enemyGroups ) {
            if ( enemyGroup.id == groupID ) {
                reference = enemyGroup;
                break;
            }
        }

        return reference;
    }

    /// <summary>
    /// Remove enemy group
    /// from the list.
    /// </summary>
    /// <param name="groupID">int - enemy group ID to remove</param>
    public void RemoveEnemyGroup( int groupID ) {
        for ( int i = 0; i < enemyGroups.Count; i++ ) {
            if ( enemyGroups[i].id == groupID ) {
                enemyGroups.RemoveAt( i );
                break;
            }
        }
    }
}
