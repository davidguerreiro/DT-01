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
    /// <param name="type">ItemData.Type - to which inventory this item will be stored</param>
    public void AddItem( GameObject item, ItemData.Type type ) {
        
        if ( ! CheckIfItemExists( item, type ) ) {

            Transform newParent;

            switch ( type ) {
                case ItemData.Type.basic:
                    newParent = basicInventory.transform;
                    break;
                case ItemData.Type.crafting:
                    newParent = craftingInventory.transform;
                    break;
                case ItemData.Type.important:
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
    /// <param name="type">ItemData.Type - to which inventory this item will be stored</param>
    /// <returns>bool</returns>
    public bool CheckIfItemExists( GameObject item, ItemData.Type type ) {
        bool result = false;
        GameObject inventoryToLooK = null;

        switch ( type ) {
            case ItemData.Type.basic:
                inventoryToLooK = basicInventory;
                break;
            case ItemData.Type.crafting:
                inventoryToLooK = craftingInventory;
                break;
            case ItemData.Type.important:
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
    /// <param name="type">ItemData.Type - to which inventory this item will be stored</param>
    /// <returns>GameObject</returns>
    public GameObject GetItem( string itemName, ItemData.Type type ) {
        GameObject item = null;
        GameObject inventoryToLook = null;

        switch ( type ) {
            case ItemData.Type.basic:
                inventoryToLook = basicInventory;
                break;
            case ItemData.Type.crafting:
                inventoryToLook = craftingInventory;
                break;
            case ItemData.Type.important:
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
    /// <param name="type">ItemData.Type - to which inventory this item will be stored</param>
    public void RemoveItem( GameObject item, ItemData.Type type  ) {
        if ( CheckIfItemExists( item, type ) ) {
            GameObject inventoryToLook = null;

            switch ( type ) {
                case ItemData.Type.basic:
                    inventoryToLook = basicInventory;
                    break;
                case ItemData.Type.crafting:
                    inventoryToLook = craftingInventory;
                    break;
                case ItemData.Type.important:
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
