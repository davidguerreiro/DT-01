using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour {
    public bool empty = true;                                   // Item box status. If empty, can be filled with an item obtained by the player.

    [Header("Components")]
    public Image itemImage;                                     // Item image component reference.
    public Sprite defaultSprite;                                // Image sprite to show when the item box is empty.
    public GameObject quantityWrapper;                          // Quantity wrapper gameObject component reference.
    public TextComponent itemQuantityText;                      // Quantity text component reference.
    public TextComponent itemNameText;                          // Item name text component reference.

    [Header("Item Instance")]
    public Item item;                                          // Item class instance saved here for reference.

    /// <summary>
    /// Add item.
    /// </summary>
    /// <parma name="item">Item - item to be stored in this item box.</param>
    /// <param name="quantity">int - current item's quantity</param>
    public void AddItem( Item item, int quantity ) {
        quantityWrapper.SetActive( true );

        // update box data with item data.
        itemImage.sprite = item.data.sprite;
        itemNameText.UpdateContent( item.data.itemName_en );
        itemQuantityText.UpdateContent( quantity.ToString() );

        // save item reference to be used by the player.
        this.item = item;
        empty = false;
    }

    /// <summary>
    /// Update quantity value.
    /// </summary>
    /// <param name="quantity">int - new quantity</param>
    public void UpdateQuantity( int quantity ) {
        itemQuantityText.UpdateContent( quantity.ToString() );
    }

    /// <summary>
    /// Remove item.
    /// </summary>
    public void RemoveItem() {
        // restore data.
        itemImage.sprite = defaultSprite;
        itemNameText.UpdateContent( "Empty" );
        itemQuantityText.UpdateContent( "0" );

        quantityWrapper.SetActive( false );

        // remove item reference.
        this.item = null;
        empty = true;
    }
}
