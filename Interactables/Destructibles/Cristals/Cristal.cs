using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cristal : MonoBehaviour {
    public float resistance;                              // Resistance - defines how many hits the cristal can take before breaking into different parts.

    [HideInInspector]
    public AudioComponent _audio;                      // Audio component class refernece.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public virtual void Init() {

        // get audio class component.
        _audio = GetComponent<AudioComponent>();
    }

    /// <summary>
    /// Cristal hit by player actions.
    /// </summary>
    /// <param name="damage">float - damage received by hit impact.</param>
    public virtual void Hit( float damage ) {
        resistance = resistance - damage;

        if ( resistance > 0 ) {

            // play sound.
            _audio.PlaySound();
        }
    }

    /// <summary>
    /// Cristal destroyed.
    /// </summary>
    public virtual void Destroyed() {

        // play destroyed sound.
        _audio.PlaySound( 1 );
    }
    
}
