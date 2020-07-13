using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticles : MonoBehaviour {

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        DestroyItself();
    }

    /// <summary>
    /// Destroy itself after the particles
    /// are displayed in the world after
    /// the impact.
    /// </summary>
    private void DestroyItself() {
        Destroy( this.gameObject, 2.5f );
    }
}
