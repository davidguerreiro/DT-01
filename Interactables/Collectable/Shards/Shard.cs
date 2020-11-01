using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : Collectable {
    public PlayerStats playerStats;                     // Data source, used to update player amount of shards.
    public int value;                                   // How many shards collected by this shard.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Collect shard.
    /// Update player's total number of shards.
    /// </summary>
    public override void Collect() {
        base.Collect();

        // play collection sound.
        if ( _audio != null ) {
            _audio.PlaySound();
        }

        // disable visible assets when the shard has been collected by the player.
        DisableModels();

        // disable any physical collider in the parent gameObject.
        Destroy( transform.parent.gameObject.GetComponent<SphereCollider>() );

        // update player shards and remove parent gameObject from the scene.
        playerStats.UpdateShards( value );
        Destroy( this.transform.parent.gameObject, 1f );

        // display shards UI if required.
        if ( ! GamePlayUI.instance.shardsComponent.displayed ) {
            GamePlayUI.instance.shardsComponent.Display();
        }
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
    protected override void Init() {
        base.Init();
    }


}
