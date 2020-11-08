using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInventory : MonoBehaviour {
    public static SceneInventory instance;                      // Class public static instance.

    [Header("Inventories")]
    public GameObject basicInventory;                           // Basic inventory gameObject.
    public GameObject craftingInventory;                        // Crafting inventory gameObject.
    public GameObject importantInventory;                       // Important inventory gameObject.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Add physical gameObject
    /// item to scene inventory.
    /// </summary>
    /// <param name="item">GameObject - item gameObject physical reference.</param>
    /// <param name="inventory">string - to which inventory this item will be stored</parma>
    public void AddItem( GameObject item, string inventory ) {
        
        if ( ! CheckIfItemExists( item, inventory ) ) {

            Transform newParent;

            switch ( inventory ) {
                case "basic":
                    newParent = basicInventory.transform;
                    break;
                case "crafting":
                    newParent = craftingInventory.transform;
                    break;
                case "important":
                    newParent = importantInventory.transform;
                    break;
                default:
                    newParent = null;
                    break;
            }

            if ( newParent != null ) {
                item.transform.parent = newParent;
                item.transform.position = newParent.position;
                item.transform.localPosition = newParent.localPosition;
            }
        }

    }

    /// <summary>
    /// Check if item exist in
    /// one of the scene inventories.
    /// </summary>
    /// <param name="item">GameObject - item gameObject physical reference.</param>
    /// <param name="inventory">string - inventory to look for the item</parma>
    /// <returns>bool</returns>
    public bool CheckIfItemExists( GameObject item, string inventory ) {
        bool result = false;
        GameObject inventoryToLooK = null;

        switch ( inventory ) {
            case "basic":
                inventoryToLooK = basicInventory;
                break;
            case "crafting":
                inventoryToLooK = craftingInventory;
                break;
            case "important":
                inventoryToLooK = importantInventory;
                break;
            default:
                inventoryToLooK = null;
                break;
        }

        if ( inventoryToLooK != null ) {
            result = inventoryToLooK.transform.Find( item.name );
        }

        return result;
    }

    /// <summary>
    /// Get item as gameObject
    /// instance.
    /// </summary>
    /// <param name="itemName">string - item gameObject name.</param>
    /// <parma name="inventory">string - inventory where search is performed</param>
    /// <returns>GameObject</returns>
    public GameObject GetItem( string itemName, string inventory ) {
        GameObject item = null;
        GameObject inventoryToLook = null;

        switch ( inventory ) {
            case "basic":
                inventoryToLook = basicInventory;
                break;
            case "crafting":
                inventoryToLook = craftingInventory;
                break;
            case "important":
                inventoryToLook = importantInventory;
                break;
            default: 
                inventoryToLook = null;
                break;
        }

        if ( inventoryToLook != null ) {
            item = inventoryToLook.transform.Find( itemName ).gameObject;
        }
        
        return item;
    }

    /// <summary>
    /// Remove item from 
    /// scene inventory.
    /// </summary>
    /// <param name="item">GameObject - item gameObject physical reference.</param>
    /// <param name="inventory">string - inventory to look for the item</parma>
    public void RemoveItem( GameObject item, string inventory ) {
        if ( CheckIfItemExists( item, inventory ) ) {
            GameObject inventoryToLook = null;

            switch ( inventory ) {
                case "basic":
                    inventoryToLook = basicInventory;
                    break;
                case "crafting":
                    inventoryToLook = craftingInventory;
                    break;
                case "important":
                    inventoryToLook = importantInventory;
                    break;
                default: 
                    inventoryToLook = null;
                    break;
            }

            if ( inventoryToLook != null ) {
                Destroy( inventoryToLook.transform.Find( item.name ).gameObject );
            }
        }
    }
    
}
