using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitParticles : MonoBehaviour {
    public ParticleSystem[] particles;                              // Particle system array reference.
    private AudioComponent _audio;                                  // Audio component reference.


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Play Enemy hit particle.
    /// </summary>
    public void DisplayHitParticles() {
        int key = UnityEngine.Random.Range( 0, ( particles.Length - 1 ) );

        if ( _audio != null ) {
            _audio.PlaySound();
        }

        if ( particles[key] != null ) {
            // particles[key].Play();
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
