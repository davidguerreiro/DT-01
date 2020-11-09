using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour {
    public int id;                                      // Enemy group ID.

    [Serializable]
    public class Enemies {                             // Data struct for enemies which belong to this group.
        public Enemy enemy;                             // Enemy reference.
        public bool isActive;                           // Flag to control whether the enemy is active in the game scene.

        public Enemies( Enemy enemy, bool isActive ) {  // Class construct.
            this.enemy = enemy;
            this.isActive = isActive;
        }
    }

    [Serializable]
    public struct SpawnData {                           // Data struct for enemies spawn in this group.
        public GameObject enemyPrefab;                  // Enemy prefab used to initialise enemy.
        public bool initActive;                         // Wheter to initialise the enemy enabled or disabled.
        public GameObject spawnPosition;                // Enemy local spawn position.
        public int round;                               // Enemy spawn round. Use -1 for infinite spawn. ( Will be execured last round if there are others using natural numbers ).
    }

    [Serializable]
    public enum SpawnType {                             // Spawn type - defines how enemies are going to be spawn by this enemy group.
        single,                                         // Only one round spawn.
        byRounds,                                       // Spawn enemies by rounds.
    }

    [Header("Enemy Spawn")]
    public SpawnData[] spawnData = new SpawnData[1];    // Spawn array of enemy prefabs and data.
    public SpawnType spawnType;                         // Spawn type for this enemy grouo.
    public bool spawnOnAwake = true;                    // Instantiate enemies on awake init. Notice that this does not mean enemies will be active in the current scene by default.

    [Header("Status")]
    public bool defeated;                               // Wheter this enemy group has been defeated.
    public bool spawn;                                  // Whether this group has already spawn enemies.
    public int currentSpawnRound = 0;                   // Current Spawn round.

    [Header("Enemies")]
    public List<Enemies> enemiesRef;                    // Enemies which belongs to this enemy group reference.


    [Header("Settings")]
    public GameObject groupPivot;                       // Pivot - this gameObject is usually represented at the center of the enemy group. Enemies will use it to calculate they movement area.
    public float maxDistance;                           // Max distance enemy can move away from pivot.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( spawnOnAwake ) {
            SpawnEnemies();    
        }    
    }


    // Start is called before the first frame update
    void Start() {
        RegisterEnemyGroup();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if ( ! defeated ) {
            CheckDefeatedStatus();
        }
    }

    /// <sumamry>
    /// Spawn enemies in the game scene.
    /// Note that enemies might not be
    /// spawn as active gameObjects and thus
    /// they will be not visible in the
    /// game scene.
    /// </summary>
    public void SpawnEnemies() {

        // instantiate enemies in scene.
        foreach ( SpawnData enemySpawn in spawnData ) {
            
            // check round.
            if ( enemySpawn.round == currentSpawnRound ) {

                // spawn enemy into gameScene.
                GameObject instance = Instantiate( enemySpawn.enemyPrefab, gameObject.transform.position, Quaternion.identity );
                instance.transform.parent = gameObject.transform;
                instance.transform.localPosition = enemySpawn.spawnPosition.transform.localPosition;

                // set up enemy data in the new instantiated enemy.
                Enemy enemy = instance.GetComponent<Enemy>();
                enemy.SetEnemyGroup( this );
                enemy.SetPublicId( GenerateEnemyID() );
                
                // add enemy to enemy refs array.
                Enemies enemyRef = new Enemies( enemy, enemySpawn.initActive );
                enemiesRef.Add( enemyRef );

                // set enemy status.
                instance.SetActive( enemySpawn.initActive );

            }
        }

        // update spawn round if spawn round is not endless spawn ( round -1 ).
        if ( spawnType == SpawnType.byRounds && currentSpawnRound > -1 ) {
            currentSpawnRound++;
        }
    }

    /// <summary>
    /// Register enemy group
    /// in local enemy groups
    /// progression.
    /// </summary>
    private void RegisterEnemyGroup() {
        LocalProgression.instance.enemyGroups.RegisterEnemyGroup( this );
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
    /// Check defeated status.
    /// </summary>
    private void CheckDefeatedStatus() {
        bool defeatedChecked = true;

        foreach ( Enemies enemyRef in enemiesRef ) {
            if ( enemyRef.isActive ) {
                defeatedChecked = false;
            }
        }

        this.defeated = defeatedChecked;
    }

    /// <summary>
    /// Alert enemies in this group.
    /// This will make enemies engage in
    /// combat with the player.
    /// </summary>
    public void AlertEnemies() {
        for ( int i = 0; i < enemiesRef.Count; i++ ) {
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
        for ( int i = 0; i < enemiesRef.Count; i++ ) {
            if ( enemiesRef[i].enemy != null && enemiesRef[i].enemy.GetPublicId() == enemyID ) {
                enemiesRef[i].isActive = false;
            }
        }
    }

    /// <summary>
    /// Remove all enemies from 
    /// this enemy group.
    /// </summary>
    public void RemoveAllEnemies() {
        for ( int i = 0; i < enemiesRef.Count; i++ ) {
            enemiesRef[i].isActive = false;
            Destroy( enemiesRef[i].enemy.gameObject );
            enemiesRef[i].enemy = null;
        }
    }

    /// <summary>
    /// Reset enemy group.
    /// </summary>
    public void ResetEnemyGroup() {
        RemoveAllEnemies();

        if ( spawnOnAwake ) {
            SpawnEnemies();
        }

        defeated = false;
    }

}
