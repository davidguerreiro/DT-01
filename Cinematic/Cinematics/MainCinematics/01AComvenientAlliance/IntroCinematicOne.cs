using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematicOne : Cinematic {

    [Header("Elements")]
    public GameObject landingSmoke;                             // Pirate's ship landing smoke effect.
    public CinematicElementGenerator generator;                 // Landing area's generator.

    // Start is called before the first frame update
    void Start() {
        // TOOD: Call init cinematic here.
    }

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
        inProgress = false;
        cinematicRoutine = null;
        yield break;
    }

}
