using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthShard : Collectable {
    public PlayerStats playerStats;                     // Data source, used to update player amount of shards.
    public float toRecover;                             // Health recover when this item is collected.
    public new GameObject model;                            // 3D shard model.
    
    private new AudioComponent _audio;                      // Audio component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Collect shard.
    /// Update player's health.
    /// This collectable can 
    /// be collected only if the player 
    /// current health is less
    /// than max health.
    /// </summary>
    public override void Collect() {
        if ( playerStats.hitPoints < playerStats.maxHitPoints ) {
            base.Collect();

            // play collection sound.
            if ( _audio != null ) {
                _audio.PlaySound();
            }

            // disable visible assets when the shard has been collected by the player.
            DisableModels();

            // disable any physical collider in the parent gameObject.
            Destroy( transform.parent.gameObject.GetComponent<SphereCollider>() );

            // update player's health and remove parent gameObject from the scene.
            Player.instance.RecoverHP( toRecover );
            Destroy( this.transform.parent.gameObject, 1f );
        }
    }

    /// <summary>
    /// Disable child gameobjects.
    /// Use when collecting the shard.
    /// </summary>
    private new void DisableModels() {
        model.SetActive( false );
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {
        
        if ( other.tag == "Player" && ! base._collided ) {
            Collect();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private new void Init() {

        // set audio component reference.
        if ( _audio == null ) {
            _audio = GetComponent<AudioComponent>();
        }   

    }
}
