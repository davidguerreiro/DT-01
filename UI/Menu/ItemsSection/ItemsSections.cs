using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSections : MonoBehaviour {

    public InventorySection basicInventorySection;                  // Basic inventory section.
    public InventorySection craftingInventorySection;               // Crafting inventory section.
    public InventorySection importantInventorySection;              // Important inventory section.

    /// <summary>
    /// Initialise section.
    /// Call this method when items
    /// section is opened in the menu.
    /// </summary>
    public void InitSection() {
        
        // update inventories.
        basicInventorySection.UpdateInventory();
        craftingInventorySection.UpdateInventory();
        importantInventorySection.UpdateInventory();
    }
}
