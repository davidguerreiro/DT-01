using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalColumn : MonoBehaviour {
    [Header("Elements")]
    public CristalLight cristalLight;                   // Crystal light class component reference.
    public RedShardEnviroment[] redShards;              // Red shards class array.

    [Header("Settings")]
    public float shardsLifeTime = 5f;                   // How many seconds the shards will stay in the game scene before being destroyed.

    void Update() {
        if ( Input.GetKeyDown( "m" ) ) {
            StartCoroutine( Disable() );
        }
    }

    /// <summary>
    /// Disable crital column.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator Disable() {

        Debug.Log( "called" );

        // switch cristal light off.
        cristalLight.SwitchOff();

        // shards stop rotating around and be dropped.
        foreach ( RedShardEnviroment redShard in redShards ) {
            redShard.Drop();
        }

        yield return new WaitForSeconds( shardsLifeTime );

        // shards to be removed from game scene.
        foreach ( RedShardEnviroment redShard in redShards ) {
            Destroy( redShard );
        }
    }
}
