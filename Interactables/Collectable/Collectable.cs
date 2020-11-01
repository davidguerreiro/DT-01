using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour {

    protected bool _collided = false;                   // Whether the obejct has been collided, thus collected by the player. This is neccesary to be used because the item is not destroyed automatically after the collision.
    public GameObject model;                            // 3D shard model.
    protected AudioComponent _audio;                    // Audio component class component reference.

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
    /// Use when collecting the shard.
    /// </summary>
    protected void DisableModels() {
        model.SetActive( false );
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
