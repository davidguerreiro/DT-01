using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cinematic : MonoBehaviour {
    public int id = 0;                                      // Cinematic ID.
    public string cinematicName;                            // Cinematic name.

    [Header("Cameras")]
    public CinematicCamera[] cameras;

    [Header("Actors")]
    public GameObject[] toDisable;                          // GameObjects to be disabled before and after the cutscene.
    public PlayerActor player;                              // This actor represents the player in the animation.
    // TODO: Incluye other actors like enemies, NPC or inanimated.
    [Header("Dialogue")]
    public DialogueContent[] dialogues;                     // Dialogues to display during cinematic.
    
    [Header("Settings")]
    public bool inGame = false;                             // Whether this cinematic happens during gameplay.

    [Header("Status")]
    public bool inProgress = false;                         // Flag to control whether the cinematic is currently in progress.
    public bool completed = false;                         // Flag to controle wheter the cinematic has already happened.

    [HideInInspector]
    public Coroutine cinematicRoutine;                      // Cinematic coroutine reference.

    /// <summary>
    /// Play cinematic.
    /// </summary>
    public abstract void PlayCinematic();

    /// <summary>
    /// Play cinematic coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected abstract IEnumerator PlayCinematicRoutine();

    /// <summary>
    /// Stop cinematic.
    /// </summary>
    public void StopCinematic() {
        if ( inProgress ) {
            inProgress = false;
        }

        if ( cinematicRoutine != null ) {
            StopCoroutine( cinematicRoutine );
            cinematicRoutine = null;
        } 
    }

    /// <summary>
    /// Start in game cinematic.
    /// </summary>
    public void StartInGame() {
        Player.instance.DisableAudio();
        GameManager.instance.inGamePlay = false;
        CinematicUI.instance.cover.FadeIn();

        foreach ( GameObject gmObject in toDisable ) {
            gmObject.SetActive(false);
        }
    }

    /// <summary>
    /// Stop in game cinematic.
    /// </summary>
    public void RestoreInGame() {
        foreach ( GameObject gmObject in toDisable ) {
            gmObject.SetActive(true);
        }

        GameManager.instance.inGamePlay = true;
        CinematicUI.instance.cover.FadeOut();
    }

}
