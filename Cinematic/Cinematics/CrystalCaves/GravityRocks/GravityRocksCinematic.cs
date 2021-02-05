using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRocksCinematic : Cinematic {

    public PasiveGravityCristal gravityCrystal;             // Gravity cristal used in the animation.
    public Animator[] rocks;                                // Animation rocks.

    private float rockVibrationSpeed = 0;                   // Rock vibration speed.

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
        
        cameras[0].SetAnimSpeed(.4f);
        cameras[0].PlayBoolAnim("shoot1");
        yield return new WaitForSeconds(1f);

        // light gravity cristal.
        gravityCrystal.LightUp();
        yield return new WaitForSeconds(2.5f);

        // move rocks.
        cameras[0].PlayBoolAnim("shoot2");
        foreach (Animator rockAnim in rocks ) {
            rockAnim.SetBool("Pulled1", true);
        }
        while ( rockVibrationSpeed < 1f ) {
            rockVibrationSpeed += 0.1f;
            foreach (Animator rockAnim in rocks ) {
                rockAnim.SetFloat("speed", rockVibrationSpeed);
            }
            yield return new WaitForSeconds(.2f);
        }

        // pull rocks towards the crystal.
        cameras[0].PlayBoolAnim("shoot3");
        yield return new WaitForSeconds(.3f);

        foreach ( Animator rockAnim in rocks ) {
            rockAnim.SetBool("Pulled2", true );
        }
        yield return new WaitForSeconds(1f);

        foreach ( Animator rockAnim in rocks ) {
            rockAnim.gameObject.GetComponent<RotateItself>().enabled = true;
        }

        yield return new WaitForSeconds(5f);

        CinematicUI.instance.cover.FadeIn();
        yield return new WaitForSeconds( .5f );
        cameras[0].gameObject.SetActive(false);
        base.RestoreInGame();

        inProgress = false;
        cinematicRoutine = null;
    }
}
