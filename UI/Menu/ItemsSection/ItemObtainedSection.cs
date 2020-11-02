using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObtainedSection : MonoBehaviour {
    private ObjectPool _notPool;                            // Object pool.

    /// <summary>
    /// Spawn object obtained 
    /// notification.
    /// </summary>
    /// <param name="item">Item - item obejct instance.</param>
    /// <param name="quantity">int - quantity obtained.</param>
    public void SpawnNotification( Item item, int quantity ) {
        // move up existing notifications.
        MoveNotsUp();

        // spawn notification.
        GameObject objectNot = _notPool.SpawnPrefab( transform.position );
        if ( objectNot != null ) {
            objectNot.GetComponent<ItemNotification>().DisplayNotification( item, quantity );
        }
    }

    /// <summary>
    /// Move up existing notifications.
    /// if required.
    /// </summary>
    private void MoveNotsUp() {
        foreach ( GameObject prefab in _notPool.pool ) {
            if ( prefab.activeSelf == true ) {
                ItemNotification itemNot = prefab.gameObject.GetComponent<ItemNotification>();

                if ( itemNot.displayed ) {
                    // move up displayed notifications.
                    itemNot.MoveUp();
                }
            }
        }
    }

}
