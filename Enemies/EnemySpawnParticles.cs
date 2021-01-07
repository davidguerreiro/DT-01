using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnParticles : MonoBehaviour {
    [Header("Particles")]
    public GameObject[] particles;                          // Enemy particles systesm gameobjects array reference.
    
    [Header("Settings")]
    public float waitForRemoving = 5f;                      // Seconds to wait till removing particles from the game scene.

    private AudioComponent _audio;                          // Audio component reference.
    private Coroutine _spawnParticlesRoutine;               // Spawn enemy particles coroutine.

    /// <summary>
    /// Spawn particles.
    /// </summary>
    public void SpawnParticles() {
        if ( _spawnParticlesRoutine == null ) {
            _spawnParticlesRoutine = StartCoroutine( SpawnParticlesCoroutine() );
        }
    }    

    /// <summary>
    /// Spawn particles coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnParticlesCoroutine() {
        if ( _audio == null ) {
            _audio = GetComponent<AudioComponent>();
            _audio.Init();
        }
        _audio.PlaySound();

        foreach( GameObject particle in particles ) {
            particle.SetActive( true );
        }

        yield return new WaitForSeconds( waitForRemoving );

        foreach( GameObject particle in particles ) {
            particle.SetActive( false );
        }
        _spawnParticlesRoutine = null;
    }

    /// <summary>
    /// Remove particle effects.
    /// </summary>
    public void Remove() {
        if ( _spawnParticlesRoutine != null ) {
            StopCoroutine( _spawnParticlesRoutine );
            _spawnParticlesRoutine = null;
        }
        foreach( GameObject particle in particles ) {
            particle.SetActive( false );
        }
    }

}
