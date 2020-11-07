using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayItemSlot : MonoBehaviour {
    [Header("Status")]
    public bool usable;                                 // Whether the item ( if any ) assigned here has stock so can be used by the player.
    public bool inUse;                                  // Whether this item is being used, so it cannot be used againg after this flag is set to false. Usually passed by reference to Item Use method.
    
    [Header("Data source")]
    public ItemAssignable assignableData;               // Item assignable data.

    [Header("Components")]
    public Image itemImage;                             // Item image component reference.
    public TextComponent quantityText;                  // Quantity text component reference.
    public Animator itemAnim;                           // Item animator component reference.
    public Sprite defaultItemImage;                     // Default item image sprite.

    private AudioComponent _audio;                      // Audio component reference.
    private Animator _anim;                             // Animator component reference.
    private Item _itemAssociated;                       // Item associated reference - used to get current quantity.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        if ( ! GameManager.instance.isPaused ) {

            ListenForUserEvents();

            if (assignableData.itemData != null ) {
                
                // if there is data in the associated ite,
                if ( _itemAssociated == null ) {
                    SetItemAssociated();
                }

                if ( _itemAssociated != null ) {
                    itemImage.sprite = _itemAssociated.data.sprite;
                    // update quantity.
                    UpdateQuantity();
                }

            } else {
                // no longer item associated to this item slot.
                usable = false;
                itemImage.sprite = defaultItemImage;
                itemAnim.SetBool( "Enabled", false );
            }
        }
    }

    /// <summary>
    /// Listen for user events.
    /// </summary>
    private void ListenForUserEvents() {
        if ( Input.GetKeyDown( assignableData.keyAssociated ) ) {
            _anim.SetBool( "Use", true );
            if ( usable && ! inUse && _itemAssociated != null && _itemAssociated.useCoroutine != null ) {
                ConsumeItem();
            } else {
                _audio.PlaySound(0);
            }
        }
    }

    /// <summary>
    /// Update quantity in game
    /// screen.
    /// </summary>
    public void UpdateQuantity() {
        int currentQuantityData = Player.instance.basicInventory.GetItemCurrentQuantity( assignableData.itemData.id );
        quantityText.UpdateContent( "x" + currentQuantityData.ToString() );

        if ( currentQuantityData == 0 ) {
            itemAnim.SetBool( "Enabled", false );
            _itemAssociated = null;
            usable = false;
        } else {
            if ( _itemAssociated == null ) {
                SetItemAssociated();
            }
            itemAnim.SetBool( "Enabled", true );
            usable = true;
        }
    }

    /// <summary>
    /// Consume assignated
    /// item.
    /// </summary>
    private void ConsumeItem() {
        if ( assignableData.itemData.itemUsageSound != null ) {
            _audio.PlayClip( assignableData.itemData.itemUsageSound, true );
        }
        _itemAssociated.Use();
    }

    /// <summary>
    /// Set reference to item associated.
    /// </summary>
    private void SetItemAssociated() {
        _itemAssociated = Player.instance.basicInventory.GetItem( assignableData.itemData.id );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();

        // get animator component reference.
        _anim = GetComponent<Animator>();

        // set up item associated.
        if ( _itemAssociated == null ) {
            SetItemAssociated();
        }
    }
}
