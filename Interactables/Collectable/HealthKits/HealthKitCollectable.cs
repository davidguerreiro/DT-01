using System.Collections;
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
                Player.instance.basicInventory.UpdateQuantity( item.data.id, currentQuantity + 1 );
            } else {
                Player.instance.basicInventory.AddItem( item );
            }

            // disable visible assets when the shard has been collected by the player.
            DisableModels();

            // disable any physical collider in the parent gameObject.
            Destroy( transform.parent.gameObject.GetComponent<SphereCollider>() );

            Destroy( this.transform.parent.gameObject, 1f );
        }
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    protected override void Init() {
        base.Init();
    }
}
