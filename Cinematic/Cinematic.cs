using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cinematic : MonoBehaviour {
    public int id = 0;                                      // Cinematic ID.
    public string cinematicName;                            // Cinematic name.

    [Header("Camera")]
    public Camera cineCamera;                               // TODO: Replace by cinematic camera controller.

    [Header("Actors")]
    public PlayerActor player;                              // This actor represents the player in the animation.
    // TODO: Incluye other actors like enemies, NPC or inanimated.

    [Header("Status")]
    public bool inProgress = false;                         // Flag to control whether the cinematic is currently in progress.
    public bool completed =  false;                         // Flag to controle wheter the cinematic has already happened.

    [HideInInspector]
    public Coroutine cinematicRoutine;                      // Cinematic coroutine reference.

    /// <summary>
    /// Play cinematic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public abstract IEnumerator PlayCinematic();
}
