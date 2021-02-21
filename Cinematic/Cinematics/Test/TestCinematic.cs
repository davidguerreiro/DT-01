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
        yield return new WaitForSeconds( 1.5f );

        cameras[0].gameObject.SetActive(true);
        CinematicUI.instance.cover.FadeOut();

        yield return new WaitForSeconds( 1f );
        
        // move player actor.
        player.moveCoroutine = StartCoroutine( player.Move( player.interactables[0].transform.position ) );
        cameras[0].SetAnimSpeed( .5f );
        cameras[0].PlayBoolAnim( "shoot1", true);

        // play dialogue.
        CinematicUI.instance.cinematicDialogue.PlayDialogue(dialogues[0]);

        do {
            yield return new WaitForFixedUpdate();
        } while ( player.isMoving || player.moveCoroutine != null );

        yield return new WaitForSeconds( 1.5f );

        // do not finish the scene if the dialogue is not closed.
        
        do {
            yield return new WaitForFixedUpdate();
        } while(CinematicUI.instance.cinematicDialogue.playing);
        


        CinematicUI.instance.cover.FadeIn();
        yield return new WaitForSeconds( .5f );
        cameras[0].gameObject.SetActive(false);
        base.RestoreInGame();

        inProgress = false;
        cinematicRoutine = null;
    }
}
