using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRocksCinematic : Cinematic {

    public PasiveGravityCristal gravityCrystal;             // Gravity cristal used in the animation.
    public Animator[] rocks;                                // Animation rocks.

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
        yield return null;
    }
}
