﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitCollectable : Collectable {
    public HealthKit item;                              // HealthKit item reference.

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
    /// Collect health kit.
    /// Add this item to player's
    /// inventory if possible.
    /// </summary>
    public override void Collect() {
        base.Collect();

        int currentQuantity = Player.instance.basicInventory.GetItemCurrentQuantity( item.data.id );

        if ( currentQuantity < item.data.maxStack ) {

            // play collection sound.
            if ( _audio != null ) {
                _audio.PlaySound();
            }

            // add item to inventory.
            if ( currentQuantity > 0 ) {
                Player.instance.basicInventory.UpdateQuantity( item.data.id );
            } else {
                Player.instance.basicInventory.AddItem( item );
            }

            // disable visible assets when the shard has been collected by the player.
            DisableModels();

            // disable particles.
            StopParticles();

            // disable any physical collider in the parent gameObject.
            if ( item.data.id == 2 ) {
                // big health kit uses capsule collider.
                Destroy( transform.parent.gameObject.GetComponent<CapsuleCollider>() );
            } else {
                Destroy( transform.parent.gameObject.GetComponent<SphereCollider>() );
            }

        }

        // Display UI notification.
        GamePlayUI.instance.itemObtainedSection.SpawnNotification( item, 1 );
        
        // TODO: Display message inventory full.
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
