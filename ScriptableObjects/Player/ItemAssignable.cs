using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssignable : ScriptableObject {
    public string keyAssociated;                        // Which key is used to trigger the assignated item usage method.
    public ItemData itemData;                           // Assignable item data reference.

    [Header("Debug")]
    public bool resetAtInit;                            // Debug - reset itemdata refernce at init.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        if ( resetAtInit ) {
            Reset();
        }
    }

    /// <summary>
    /// Reset item assignable.
    /// </summary>
    public void Reset() {
        Debug.Log( "reset called" );
        itemData = null;
    }
}
