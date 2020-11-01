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

    public class Craft {                                                        // Crafting items class.
        public Item item;
        public int quantity;

        public Craft( Item item, int quantity ) {
            this.item = item;
            this.quantity = quantity;
        }
    }

    public class Important {                                                    // Important items class.
        public Item item;
        public int quantity;

        public Important( Item item, int quantity ) {
            this.item = item;
            this.quantity = quantity;
        }                       
    }

    public List<Usable> usableInventory = new List<Usable>();                   // List inventory of usable items.
    public List<Craft> craftInventory = new List<Craft>();                      // List inventory of craft items.
    public List<Important> importantInventory = new List<Important>();          // List inventory of important items.

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

    /// <summary>
    /// Add crafting item.
    /// </summary>
    /// <param name="item">Item - crafting item to store in the inventory.</param>
    /// <param name="quantity">int - how many units will be stack in the inventory</param>
    /// <returns>bool</returns>
    public bool AddCraftingItem( Item item, int quantity = 1 ) {
        for ( int i = 0; i < craftInventory.Count; i++ ) {
            if ( craftInventory[i].item.data.id == item.data.id ) {
                // update quantity.
                craftInventory[i].quantity += quantity;

                // check if no more items can be stack.
                if ( craftInventory[i].quantity > craftInventory[i].item.data.maxStack ) {
                    craftInventory[i].quantity = craftInventory[i].item.data.maxStack;
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

        Craft newItem = new Craft( item, quantity );
        craftInventory.Add( newItem );

        return allAdded;
    }

    /// <summary>
    /// Add important item.
    /// </summary>
    /// <param name="item">Item - important item to store in the inventory.</param>
    /// <param name="quantity">int - how many units will be stack in the inventory</param>
    /// <returns>bool</returns>
    public bool AddImportantItem( Item item, int quantity = 1 ) {
        for ( int i = 0; i < importantInventory.Count; i++ ) {
            if ( importantInventory[i].item.data.id == item.data.id ) {
                // update quantity.
                importantInventory[i].quantity += quantity;

                // check if no more items can be stack.
                if ( importantInventory[i].quantity > importantInventory[i].item.data.maxStack ) {
                    importantInventory[i].quantity = importantInventory[i].item.data.maxStack;
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

        Important newItem = new Important( item, quantity );
        importantInventory.Add( newItem );

        return allAdded;
    }

    /// <sumamry>
    /// Update quantity.
    /// If the item stack is 0,
    /// the item will be removed from
    /// the inventary.
    /// </summary>
    /// <param name="itemID">int - item id.</param>
    /// <param name="type">ItemData.type - item type.</param>
    /// <param name="newQuantity">int - new item quantity. Default value to 1</param>
    /// <returns>string</returns>
    public string UpdateQuantity( int itemID, ItemData.Type type, int quantity = 1 ) {
        string response = "Not found";

        if ( type == ItemData.Type.basic ) {                // Usable item type logic 
            for ( int i = 0; i < usableInventory.Count; i++ ) {
                if ( itemID == usableInventory[i].item.data.id ) {
                    
                    // check if full.
                    if ( usableInventory[i].quantity == usableInventory[i].item.data.maxStack ) {
                        response = "full";
                        return response;
                    }

                    usableInventory[i].quantity += quantity;

                    // check if over stack.
                    if ( usableInventory[i].quantity > usableInventory[i].item.data.maxStack ) {
                        usableInventory[i].quantity = usableInventory[i].item.data.maxStack;
                        response = "added";
                        return response;
                    }

                    // check if no more stack, then remove item.
                    if ( usableInventory[i].quantity <= 0 ) {
                        bool removed = RemoveItem( itemID, type );
                        response = ( removed ) ? "removed" : "Not found";
                        return response;
                    }
                    
                }
            }
        } else if ( type == ItemData.Type.crafting ) {                          // Crafting item type logic.
            for ( int i = 0; i < craftInventory.Count; i++ ) {
                if ( itemID == craftInventory[i].item.data.id ) {
                    
                    // check if full.
                    if ( craftInventory[i].quantity == craftInventory[i].item.data.maxStack ) {
                        response = "full";
                        return response;
                    }

                    craftInventory[i].quantity += quantity;

                    // check if over stack.
                    if ( craftInventory[i].quantity > craftInventory[i].item.data.maxStack ) {
                        craftInventory[i].quantity = craftInventory[i].item.data.maxStack;
                        response = "added";
                        return response;
                    }

                    // check if no more stack, then remove item.
                    if ( craftInventory[i].quantity <= 0 ) {
                        bool removed = RemoveItem( itemID, type );
                        response = ( removed ) ? "removed" : "Not found";
                        return response;
                    }
                    
                }
            }
        } else if ( type == ItemData.Type.important ) {     // Important item type logic.
            for ( int i = 0; i < importantInventory.Count; i++ ) {
                if ( itemID == importantInventory[i].item.data.id ) {
                    
                    // check if full.
                    if ( importantInventory[i].quantity == importantInventory[i].item.data.maxStack ) {
                        response = "full";
                        return response;
                    }

                    importantInventory[i].quantity += quantity;

                    // check if over stack.
                    if ( importantInventory[i].quantity > importantInventory[i].item.data.maxStack ) {
                        importantInventory[i].quantity = importantInventory[i].item.data.maxStack;
                        response = "added";
                        return response;
                    }

                    // check if no more stack, then remove item.
                    if ( importantInventory[i].quantity <= 0 ) {
                        bool removed = RemoveItem( itemID, type );
                        response = ( removed ) ? "removed" : "Not found";
                        return response;
                    }  
                }
            }
        } else {
            response = "Invalid item type";
        }

        return response;
    }
    

    /// <summary>
    /// Remove item from 
    /// the player's inventary.
    /// </summary>
    /// <param name="itemID">int - item id</param>
    /// <param name="type">ItemData.Type - item type.</param>
    /// <returns>bool</returns>
    public bool RemoveItem( int itemID, ItemData.Type type ) {
        int index = 0;
        if ( type == ItemData.Type.basic ) {                    // Remove item from basic usable items.
            for ( int i = 0; i < usableInventory.Count; i++ ) {
                if ( usableInventory[i].item.data.id == itemID ) {
                    index = i;
                }
            }

            usableInventory.RemoveAt( index );
            return true;
        }
        else if ( type == ItemData.Type.crafting ) {            // Remove item from crafting usable items.
            for ( int i = 0; i < craftInventory.Count; i++ ) {
                if ( craftInventory[i].item.data.id == itemID ) {
                    index = i;
                }
            }

            craftInventory.RemoveAt( index );
            return true;
        } else if ( type == ItemData.Type.important ) {         // Remove item from important usable items.
            for ( int i = 0; i < importantInventory.Count; i++ ) {
                if ( importantInventory[i].item.data.id == itemID ) {
                    index = i;
                }
            }

            importantInventory.RemoveAt( index );
            return true;
        }

        return false;
    }

    /// <summary>
    /// Get usable item.
    /// </sumamry>
    /// <param name="itemId">int - itemID.</param>
    /// <returns>UsableItem</returns>
    public UsableItem GetUsableItem( int itemId ) {
        UsableItem usableItem = null;
        
        for ( int i = 0; i < usableInventory.Count; i++ ) {
            if ( usableInventory[i].item.data.id == itemId ) {
                usableItem = usableInventory[i].item;
                break;
            }
        }

        return usableItem;
    }

    /// <summary>
    /// Get crafting item.
    /// </sumamry>
    /// <param name="itemId">int - itemID.</param>
    /// <returns>Item</returns>
    public Item GetCraftingItem( int itemId ) {
        Item craftingItem = null;
        
        for ( int i = 0; i < craftInventory.Count; i++ ) {
            if ( craftInventory[i].item.data.id == itemId ) {
                craftingItem = craftInventory[i].item;
                break;
            }
        }

        return craftingItem;
    }

    /// <summary>
    /// Get important item.
    /// </sumamry>
    /// <param name="itemId">int - itemID.</param>
    /// <returns>Item</returns>
    public Item GetImportantItem( int itemId ) {
        Item importantItem = null;
        
        for ( int i = 0; i < importantInventory.Count; i++ ) {
            if ( importantInventory[i].item.data.id == itemId ) {
                importantItem = importantInventory[i].item;
                break;
            }
        }

        return importantItem;
    }
}
