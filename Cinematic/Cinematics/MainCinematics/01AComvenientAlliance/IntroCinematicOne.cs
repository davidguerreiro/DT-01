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

    private AudioComponent _spaceShipAudio;                     // Spaceship audio component.


    // Start is called before the first frame update
    void Start() {
        PlayCinematic();
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

        // ----- Initial shoots.

        // play music.
        LevelManager.instance.levelMusicController.PlaySong("scene", "introMusic");
        yield return new WaitForSeconds(2f);

        // play camera animations and remove cinematic UI cover.
        cameras[0].PlayBoolAnim("Intro1Shoots", true);
        CinematicUI.instance.cover.FadeOut(.1f);
        yield return new WaitForSeconds(3f);

        // play generator animations.
        generator.audio.PlaySound();
        generator.anim.SetTrigger("StartEngine");
        yield return new WaitForSeconds(2f);

        generator.anim.enabled = false;
        generator.RotateFan();
        yield return new WaitForSeconds(1.5f);
        generator.sparksAudio.PlaySound(0);
        generator.sparks.Play();
        yield return new WaitForSeconds(.5f);
        generator.sparks.Stop();
        yield return new WaitForSeconds(1.5f);
        generator.sparksAudio.PlaySound(1);
        generator.sparks.Play();
    
        // wait till initial shoots are done.
        do {
            yield return new WaitForFixedUpdate();
        } while (cameras[0].events[0]);

        // ------ Spacehip arrival and landing.
        spaceShip.gameObject.SetActive(true);
        _spaceShipAudio = spaceShip.gameObject.GetComponent<AudioComponent>();
        _spaceShipAudio.PlaySound();
        spaceShip.SetBool("Navigate", true);
        yield return new WaitForSeconds(2.2f);

        cameras[0].PlayBoolAnim("Intro2Shoots", true);


        inProgress = false;
        cinematicRoutine = null;
        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {
        // get actors and elements additional components.
       // _spaceShipAudio = spaceShip.gameObject.GetComponent<AudioComponent>();
    }
}
