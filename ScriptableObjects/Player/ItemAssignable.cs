using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssignable : ScriptableObject {
    public KeyCode keyAssociated;                       // Which key is used to trigger the assignated item usage method.
    public ItemData itemData;                           // Assignable item data reference.

    [Header("Debug")]
    public bool resetAtInit;                            // Debug - reset itemdata refernce at init.

    /// <summary>
    /// Reset item assignable.
    /// </summary>
    public void Reset() {
        Debug.Log( "reset called" );
        itemData = null;
    }
}
