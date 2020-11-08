using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour {
    // public bool destructible;                           // Whether to destroy this collectable after being collected by the player. Stackable items ( like health kits ) should not be destroyed so a reference to the item can be kept in the inventory.
    public GameObject model;                            // 3D shard model.
    public ParticleSystem particles;                    // Particles used in this collectable.
    protected AudioComponent _audio;                    // Audio component class component reference.
    protected bool _collided = false;                   // Whether the obejct has been collided, thus collected by the player. This is neccesary to be used because the item is not destroyed automatically after the collision.

    /// <summary>
    /// Collect method.
    /// All collectibles can be collected,
    /// so this method is mandatory in every
    /// collectible.
    /// </summary>
    public virtual void Collect() {
        _collided = true;
    }

    /// <summary>
    /// Disable child gameobjects.
    /// Use when collecting this collectable.
    /// </summary>
    protected void DisableModels() {
        if ( model != null ) {
            model.SetActive( false );
        }
    }

    /// <summary>
    /// Stop particle system.
    /// </summary>
    protected void StopParticles() {
        if ( particles != null ) {
            particles.Pause();
        }
    } 

    /// <summary>
    /// Init class method.
    /// </summary>
    protected virtual void Init() {

        // set audio component reference.
        if ( _audio == null ) {
            _audio = GetComponent<AudioComponent>(); 
        }  
    } 
}
