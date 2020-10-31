using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour {

    public enum Type {
        stackable,
        nonStackable,
    };

    public Type type;                                   // Collectible type. Stackable for inventary-type collectables and non-stackable for collecting only items like shards.
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
    
}
