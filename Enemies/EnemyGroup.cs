using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour {
    public int id;                                      // Enemy group ID.

    [Serializable]
    public struct Enemies {                             // Data struct for enemies which belong to this group.
        public Enemy enemy;                             // Enemy reference.
        public bool isActive;                           // Flag to control whether the enemy is active in the game scene.
    }

    [Header("Enemies")]
    public Enemies[] enemiesRef = new Enemies[1];       // Enemies which belongs to this enemy group reference.

    [Header("Settings")]
    public GameObject groupPivot;                       // Pivot - this gameObject is usually represented at the center of the enemy group. Enemies will use it to calculate they movement area.
    public float maxDistance;                           // Max distance enemy can move away from pivot.


    // Start is called before the first frame update
    void Start() {
        SetUpEnemies();
    }

    /// <sumamry>
    /// Set up enemy group and id.
    /// </summary>
    public void SetUpEnemies() {

        foreach ( Enemies enemyRef in enemiesRef ) {
            enemyRef.enemy.SetEnemyGroup( this );
            enemyRef.enemy.SetPublicId( GenerateEnemyID() );
        }
    }

    /// <summary>
    /// Generate enemy ID.
    /// </summary>
    public int GenerateEnemyID() {
        int temp = 0;
        string numberStr = "";
        
        // id size = 15.
        for ( int i = 0; i < 8; i++ ) {
            temp = UnityEngine.Random.Range( 0, 9 );
            numberStr += temp.ToString();
        }

        return int.Parse( numberStr );
    }

    /// <summary>
    /// Alert enemies in this group.
    /// This will make enemies engage in
    /// combat with the player.
    /// </summary>
    public void AlertEnemies() {
        for ( int i = 0; i < enemiesRef.Length; i++ ) {
            if ( enemiesRef[i].enemy != null && enemiesRef[i].enemy.GetState() != Enemy.State.returning ) {
                enemiesRef[i].enemy.SetState( Enemy.State.battling );
            }
        }
    }

    /// <summary>
    /// Disable enemy from the group.
    /// This usually happens when an enemy is defeated.
    /// </summary>
    /// <param name="enemyID">int - enemy unique ID</param>
    public void DisableEnemy( int enemyID ) {
        for ( int i = 0; i < enemiesRef.Length; i++ ) {
            if ( enemiesRef[i].enemy != null && enemiesRef[i].enemy.GetPublicId() == enemyID ) {
                enemiesRef[i].isActive = false;
                enemiesRef[i].enemy = null;
            }
        }
    }

    /// <summary>
    /// Remove all enemies from game scene.
    /// </summary>
    public void RemoveAllEnemies() {
        for ( int i = 0; i < enemiesRef.Length; i++ ) {
            enemiesRef[i].isActive = false;
            Destroy( enemiesRef[i].enemy.gameObject );
            enemiesRef[i].enemy = null;
        }
    }

}
