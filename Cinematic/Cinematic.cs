using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cinematic : MonoBehaviour {
    public int id = 0;                                      // Cinematic ID.
    public string cinematicName;                            // Cinematic name.

    [Header("Camera")]
    public Animator cameraAnim;                             // Camera animator.

    [Header("Actors")]
    public GameObject[] toDisable;                          // GameObjects to be disabled before and after the cutscene.
    public PlayerActor player;                              // This actor represents the player in the animation.
    // TODO: Incluye other actors like enemies, NPC or inanimated.
    // TODO: Incluye UI elements like screen cover and dialogue system reference.
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
    /// <returns>IEnumerator</returns>
    protected abstract IEnumerator PlayCinematic();

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
        GameManager.instance.inGamePlay = false;
        GameManager.instance.PauseGame();
    }

}
