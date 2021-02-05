using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCinematic : Cinematic {

    /// <summary>
    /// Play cinematic.
    /// </summary>
    public override void PlayCinematic() {
        if ( cinematicRoutine == null ) {
            Debug.Log("triggered");
            cinematicRoutine = StartCoroutine(PlayCinematicRoutine());
        }
    }

    /// <summary>
    /// Play cinematic coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator PlayCinematicRoutine() {
        inProgress = true;
        yield return new WaitForSecondsRealtime( 2.5f );
        
        // move player actor.
        player.moveCoroutine = StartCoroutine( player.Move( player.interactables[0].transform.position ) );

        do {
            yield return new WaitForFixedUpdate();
        } while ( player.isMoving || player.moveCoroutine != null );

        inProgress = false;
        cinematicRoutine = null;
    }
}
