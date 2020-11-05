using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour {
    public bool empty = true;                                   // Item box status. If empty, can be filled with an item obtained by the player.

    [Header("Components")]
    public Image itemImage;                                     // Item image component reference.
    public Image background;                                    // Item background image component reference.
    public Sprite defaultSprite;                                // Image sprite to show when the item box is empty.
    public GameObject quantityWrapper;                          // Quantity wrapper gameObject component reference.
    public TextComponent itemQuantityText;                      // Quantity text component reference.
    public TextComponent itemNameText;                          // Item name text component reference.

    [Header("Settings")]
    public Color backgroundDefaultColor;                        // Background color used by default.
    public Color textDefaultColor;                              // Item name color used by default.
    public Color noEmptyBackgroundColor;                        // Background color used when the item box has an item inside.
    public Color noEmptyTextColor;                              // Item name color used when the item box has an item inside. 

    [Header("Item Instance")]
    public Item item;                                           // Item class instance saved here for reference.

    private ItemsSections _itemsSections;                       // Item sections class component reference.
    private Animator _anim;                                     // Animator component reference.
    private AudioComponent _audio;                              // Audio component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

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

        // update box UI.
        background.color = noEmptyBackgroundColor;
        itemNameText.UpdateColour( noEmptyTextColor );

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

        // update UI to original values.
        background.color = backgroundDefaultColor;
        itemNameText.UpdateColour( textDefaultColor );

        // remove item reference.
        this.item = null;
        empty = true;
    }

    /// <summary>
    /// Hover logic.
    /// This method is triggered from
    /// event system component.
    /// </summary>
    public void HoverIn() {
        if ( ! empty && item != null ) {
            _audio.PlaySound(0);
            _anim.SetBool( "Hover", true );

            // update sidebar description data.
            if ( _itemsSections != null ) {
                _itemsSections.descriptionSection.UpdateSection( item.data.description_en, item.data.sprite );
            }
        }
    }

    /// <summary>
    /// Hover out logic.
    /// This method is triggered from
    /// event system component.
    /// </summary>
    public void HoverOut() {
        if ( ! empty && item != null ) {
            _anim.SetBool( "Hover", false );
        }
    }
    

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get items section component reference.
        if ( _itemsSections == null ) {
            _itemsSections = GetComponentInParent<ItemsSections>();
        }

        // get audio component.
        if ( _audio == null ) {
            _audio = GetComponent<AudioComponent>();
        }

        // get animator component.
        if ( _anim == null ) {
            _anim = GetComponent<Animator>();
        }
    }

}
