using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour {

    public enum Type {
        stackable,
        nonStackable,
    };

    public Type type;                                   // Collectible type. Stackable for inventary-type collectables and non-stackable for collecting only items like shards.
    
}
