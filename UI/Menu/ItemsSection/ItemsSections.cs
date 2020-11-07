using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSections : MonoBehaviour {

    [Header("Inventary Sections")]
    public InventorySection basicInventorySection;                  // Basic inventory section.
    public InventorySection craftingInventorySection;               // Crafting inventory section.
    public InventorySection importantInventorySection;              // Important inventory section.

    [Header("Sidebar")]
    public MenuDescriptionSection descriptionSection;               // Description section. 

    /// <summary>
    /// Initialise section.
    /// Call this method when items
    /// section is opened in the menu.
    /// </summary>
    public void InitSection() {
        
        Debug.Log( "called init" );
        // update inventories.
        basicInventorySection.UpdateInventory();
        craftingInventorySection.UpdateInventory();
        importantInventorySection.UpdateInventory();
    }
}
