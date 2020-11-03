using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObtainedSection : MonoBehaviour {
    private ObjectPool _notPool;                            // Object pool.
    private RectTransform _rect;                            // Rect transform.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

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
        GameObject objectNot = _notPool.SpawnPrefab( _rect.transform.localPosition );
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

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get object pool component reference.
        _notPool = GetComponent<ObjectPool>();

        // get rect transform component reference.
        _rect = GetComponent<RectTransform>();
    }

}
