using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventary : ScriptableObject {
    public struct Usable {                          // Usabel items struct.
        UsableItem item;
        int quantity;
    };

    public List<Usable> usableInventory = new List<Usable>();                   // List inventory of usable items.

    /// <summary>
    /// Add item in one of the 
    /// inventory lists.
    /// </summary>

}
