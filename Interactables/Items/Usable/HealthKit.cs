using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : Item {
    public float toRecover;                                     // Health recovered by this healht kit.

    private AudioComponent _audio;                      // Audio component reference

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }
        
    /// <summary>
    /// Use item action.
    /// </summary>
    public override void Use() {
        if ( useCoroutine == null ) {
            useCoroutine = StartCoroutine( UseRoutine() );
        }
    }

    /// <summary>
    /// Use item action coroutine.
    /// Item use logic goes here.
    /// </summary>
    /// <parma name="audioSource">AudioComponent - Audio source to play sounds. Not required.</param>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator UseRoutine() {
        Debug.Log( data.itemName_en + " used!" );
        
        if ( Player.instance.playerData.hitPoints < Player.instance.playerData.maxHitPoints ) {
            // heal player.
            Player.instance.RecoverHP( toRecover, true, data.itemUsageSound );

            // discount item from inventory.
            int quantity = Player.instance.basicInventory.GetItemCurrentQuantity( data.id );
            Player.instance.basicInventory.UpdateQuantity( data.id, -1 );
        } else {
            _audio.PlaySound( 2 );
        }
        
        useCoroutine = null;
        yield break;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
