using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCinematic : Cinematic {

    // Start is called before the first frame update
    void Start() {
        cinematicRoutine = StartCoroutine( PlayCinematic() );
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Play cinematic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public override IEnumerator PlayCinematic() {
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
