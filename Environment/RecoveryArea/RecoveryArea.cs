using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryArea : MonoBehaviour {
    public float toRecover = 5f;                            // Amount of hp recovered in this recovery area.
    public float interval = 1f;                             // Interval of time - seconds passed between each recovery.             

    private Coroutine _recoverHp;                           // Recover hp coroutine.

    /// <summary>
    /// Recovers player's hp
    /// as long as it stands in 
    /// this entity.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator RecoverHp() {
        Player player = Player.instance;

        if ( player != null ) {

            while ( player.playerData.hitPoints < player.playerData.maxHitPoints ) {
                player.RecoverHP( toRecover );

                yield return new WaitForSeconds( interval );
            }

            _recoverHp = null;
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {
        
        if ( other.gameObject.tag == "Player" && _recoverHp == null ) {
            _recoverHp = StartCoroutine( "RecoverHp" );
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit( Collider other ) {
        
        if ( other.gameObject.tag == "Player" && _recoverHp != null ) {
            StopCoroutine( _recoverHp );
            _recoverHp = null;
        }
    }
}
