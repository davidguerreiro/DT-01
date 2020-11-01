using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject {
    public class Usable {                                                       // Usabel items class.
        public UsableItem item;
        public int quantity;

        public Usable( UsableItem item, int quantity ) {
            this.item = item;
            this.quantity = quantity;
        }
    };

    public List<Usable> usableInventory = new List<Usable>();                   // List inventory of usable items.

    /// <summary>
    /// Add usable item.
    /// </summary>
    /// <param name="item">UsableItem usable item to store in the inventory.</param>
    /// <param name="quantity">int - how many units will be stack in the inventory</param>
    /// <returns>bool</returns>
    public bool AddUsableItem( UsableItem item, int quantity = 1 ) {
        for ( int i = 0; i < usableInventory.Count; i++ ) {
            if ( usableInventory[i].item.data.id == item.data.id ) {
                // update quantity.
                usableInventory[i].quantity += quantity;

                // check if no more items can be stack.
                if ( usableInventory[i].quantity > usableInventory[i].item.data.maxStack ) {
                    usableInventory[i].quantity = usableInventory[i].item.data.maxStack;
                    return false;
                }
                return true;
            }
        }

        // if here, item is not in the inventory.
        bool allAdded = true;
        if ( quantity > item.data.maxStack ) {
            allAdded = false;
            quantity = item.data.maxStack;
        }

        Usable newItem = new Usable( item, quantity );
        usableInventory.Add( newItem );

        return allAdded;
    }

    /// <sumamry>
    /// Update quantity.
    /// If the item stack is 0,
    /// the item will be removed from
    /// the inventary.
    /// </summary>
}
