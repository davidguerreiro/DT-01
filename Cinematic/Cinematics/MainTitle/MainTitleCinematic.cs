using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleCinematic : Cinematic {

    /// <summary>
    /// Play cinematic wrapper.
    /// </summary>
    public void Play() {
        if ( ! inProgress && cinematicRoutine == null ) {
            cinematicRoutine = StartCoroutine( PlayCinematic() );
        }
    }
    
    /// <summary>
    /// Play cinematic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator PlayCinematic() {
        inProgress = true;
        
        // move player actor.
        player.moveCoroutine = StartCoroutine( player.Move( player.interactables[0].transform.position ) );

        do {
            yield return new WaitForFixedUpdate();
        } while ( player.isMoving || player.moveCoroutine != null );

        inProgress = false;
        cinematicRoutine = null;
    }
}
