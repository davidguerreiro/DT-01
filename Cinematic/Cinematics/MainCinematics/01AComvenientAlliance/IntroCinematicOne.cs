using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematicOne : Cinematic {

    [Header("Elements")]
    public GameObject landingSmoke;                             // Pirate's ship landing smoke effect.
    public CinematicElementGenerator generator;                 // Landing area's generator.

    [Header("Main Actors")]
    public Animator spaceShip;                                  // Pirates spaceship anim reference.
    public Animator probotLeft;                                 // Left Probot anim.
    public Animator probotRight;                                // Left Probot anim.


    // Start is called before the first frame update
    void Start() {
        PlayCinematicRoutine();
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
        yield return new WaitForSeconds(1f);

        // play music.
        LevelManager.instance.levelMusicController.PlaySong("scene", "introMusic");
        yield return new WaitForSeconds(1f);

        // play camera animations and remove cinematic UI cover.
        cameras[0].PlayBoolAnim("Intro1Shoots", true);
        CinematicUI.instance.cover.FadeOut();
        yield return new WaitForSeconds(2f);

        // play generator animations.
        generator.audio.PlaySound();
        generator.anim.SetTrigger("StartEngine");
        yield return new WaitForSeconds(2f);

        generator.RotateFan();
        yield return new WaitForSeconds(1.5f);
        generator.sparksAudio.PlaySound(0);
        generator.sparks.Play();
        yield return new WaitForSeconds(.5f);
        generator.sparks.Stop();
        yield return new WaitForSeconds(1.5f);
        generator.sparksAudio.PlaySound(1);
        generator.sparks.Play();

        inProgress = false;
        cinematicRoutine = null;
        
    }

}
