using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticles : MonoBehaviour {
    [Header("Particle systems")]
    public ParticleSystem[] particles;                              // Particle system array reference.

    [Header("Settings")]
    public float timeInBetween = .1f;                               // Time to wait between each particle system is played.

    private AudioComponent _audio;                                  // Audio component reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Play death enemy particle effect.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator DisplayDeathParticles() {
        
        if ( _audio ) {
            _audio.PlaySound();
        }

        foreach ( ParticleSystem particleSystem in particles ) {
            particleSystem.Play();

            yield return new WaitForSeconds( timeInBetween );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
    
}
