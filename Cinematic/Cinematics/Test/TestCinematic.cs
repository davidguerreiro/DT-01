using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCinematic : Cinematic {

    /// <summary>
    /// Play cinematic.
    /// </summary>
    public override void PlayCinematic() {
        if ( cinematicRoutine == null ) {
            cinematicRoutine = StartCoroutine(PlayCinematicRoutine());
        }
    }

    /// <summary>
    /// Play cinematic coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator PlayCinematicRoutine() {
        inProgress = true;
        base.StartInGame();
        yield return new WaitForSecondsRealtime( 1.5f );

        cameras[0].gameObject.SetActive(true);
        CinematicUI.instance.cover.FadeOut();

        yield return new WaitForSecondsRealtime( 1f );
        
        // move player actor.
        player.moveCoroutine = StartCoroutine( player.Move( player.interactables[0].transform.position ) );
        cameras[0].SetAnimSpeed( .5f );
        cameras[0].PlayBoolAnim( "shoot1", true);

        do {
            yield return new WaitForSecondsRealtime(0.1f);
        } while ( player.isMoving || player.moveCoroutine != null );

        yield return new WaitForSecondsRealtime( 1.5f );
        CinematicUI.instance.cover.FadeIn();
        yield return new WaitForSecondsRealtime( .5f );
        base.RestoreInGame();

        inProgress = false;
        cinematicRoutine = null;
    }
}
