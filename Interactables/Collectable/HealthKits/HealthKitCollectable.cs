using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitCollectable : Collectable {
    public HealthKitData data;                              // HealthKit item data.

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

        // TODO: Check if player can collect this health kit and call Add to inventory here.

        // play collection sound.
        if ( _audio != null ) {
            _audio.PlaySound();
        }

        // disable visible assets when the shard has been collected by the player.
        DisableModels();

        // disable any physical collider in the parent gameObject.
        Destroy( transform.parent.gameObject.GetComponent<SphereCollider>() );

        Destroy( this.transform.parent.gameObject, 1f );
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    protected override void Init() {
        base.Init();
    }
}
