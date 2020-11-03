using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySection : MonoBehaviour {
    public Inventory inventory;                             // Inventory data source.
    public ItemBox[] itemBoxes = new ItemBox[12];           // Inventory item boxes reference.

    
    /// <summary>
    /// Refresh inventory.
    /// Call this method each time 
    /// the inventory changes.
    /// </summary>
    public void UpdateInventory() {
        // clean up inventory UI.
        ClearInventorySection();

        for ( int i = 0; i < inventory.items.Count; i++ ) {
            itemBoxes[i].AddItem( inventory.items[i].item, inventory.items[i].quantity );
        }
    }

    /// <summary>
    /// Clear UI inventory.
    /// Usually called every time
    /// the inventory is updated.
    /// </summary>
    private void ClearInventorySection() {
        foreach ( ItemBox itemBox in itemBoxes ) {
            itemBox.RemoveItem();
        }
    }
}
