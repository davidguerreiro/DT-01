using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicElementGenerator : MonoBehaviour {

    [Header("Components")]
    public Rotator fan;                                    // Fan rotator class.
    public ParticleSystem sparks;                          // Sparks particle system
    public AudioComponent sparksAudio;                      // Sparks audio component.

    [Header("Settings")]
    public float maxSpeed;

    [HideInInspector]
    public AudioComponent audio;                          // Audio component reference.

    [HideInInspector]
    public Animator anim;                                 // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init fan rotation.
    /// </summary>
    public void RotateFan() {
        StartCoroutine(RotateFanRoutine());
    }

    /// <summary>
    /// Init fan rotation coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator RotateFanRoutine() {
        while (fan.speed < maxSpeed) {
            fan.speed += 1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        audio = GetComponent<AudioComponent>();
        anim = GetComponent<Animator>();
    }
}
