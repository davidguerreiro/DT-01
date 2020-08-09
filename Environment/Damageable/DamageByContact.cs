using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByContact : MonoBehaviour {

    [Header("Values")]
    public float damage = 5f;                            // How much damage causes to other entities.
    public float interval = 2f;                          // Interval to calculate how ofter damage is set to entities whilist the entity is in contact with this object.

    [Header("Settings")]
    public bool damageEnemies = true;                    // Wheter this object will damage enemies on contact. False to cause damage only to the player.

    private Coroutine _causeDamage;                      // Coroutine to cause damage by interval.

    /// <summary>
    /// Cause damage to entity as long
    /// as the entity is in contact with
    /// this object.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator CauseDamage() {

        Player player = Player.instance;

        if ( player != null ) {

            while ( player.playerData.hitPoints > 0f ) {

                // update player hitPoints.
                player.GetDamage( - damage );

                yield return new WaitForSecondsRealtime( interval );
            }
            
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {
        
        // TODO: Add enemy damage when the enemies have been developed.
        if ( other.gameObject.tag == "Player" && _causeDamage == null ) {
            _causeDamage = StartCoroutine( "CauseDamage" );
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit( Collider other ) {
        
        // TODO: Add enemy damage when the enemies have been developed.
        if ( _causeDamage != null ) {
            StopCoroutine( _causeDamage );
            _causeDamage = null;
        }
    }
}
