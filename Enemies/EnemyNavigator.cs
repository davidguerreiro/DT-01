using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator : MonoBehaviour {
    
    [Header("Settings")]
    [SerializeField]
    protected float maxDistance = 10f;                             // Max distance for enemy detection vision.

    [HideInInspector]
    public RaycastHit hit;                                         // Hit used 
    
    private Enemy enemyParent;                                     // Enemy to which this navigator belongs to.                                            

    void FixedUpdate() {

        if ( enemyParent != null ) {
            TriggerVisionRay();
        }
    }

    /// <summary>
    /// Trigger raycast used by the enemy to
    /// detect other agents in the game.
    /// </summary>
    public virtual void TriggerVisionRay() {
        // debug - comment of remove.
        Debug.DrawRay( transform.position, transform.TransformDirection( Vector3.forward ) * maxDistance, Color.red );

        if ( Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out hit, maxDistance ) ) {
            if ( hit.collider.gameObject.tag == "Player" ) {
                Debug.Log( "player detected" );
            }
        }
    }

    /// <sumamry>
    /// Set up and link navigator to
    /// parent enemy.
    /// </summary>
    public void SetUpNavigator( Enemy enemy ) {
        this.enemyParent = enemy;
    }
}
