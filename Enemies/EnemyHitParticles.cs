using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitParticles : MonoBehaviour {
    public ParticleSystem[] particles;                              // Particle system array reference.

    /// <summary>
    /// Play Enemy hit particle.
    /// </summary>
    public void DisplayHitParticles() {
        int key = UnityEngine.Random.Range( 0, ( particles.Length - 1 ) );

        if ( particles[key] != null ) {
            particles[key].Play();
        }
    }
}
