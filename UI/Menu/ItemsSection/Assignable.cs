using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assignable : MonoBehaviour {
    public bool empty;                                      // Whether this assignable slot is ready to get an item assigned.
    
    [Header("Assignable data")]
    public ItemAssignable assignableData;                   // Item assignable data.

    [Header("Components")]
    public Image itemImage;                                 // Item image displayed in this assignable component.
    public Animator itemImageAnim;                          // Item image animator component reference.
    public Sprite defaultSprite;                            // Default sprite.

    private Animator _anim;                                 // Anim component reference.
    private AudioComponent _audio;                          // Audio component reference.
    private bool _cursorIn = false;                          // Flag to control wheter the cursor is hovering this assignable slot.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if ( ! empty ) {
            if ( _cursorIn && Input.GetMouseButton(1) ) {
                RemoveItem();
                _anim.SetBool( "Hover", false );
            }
        }
    }

    /// <summary>
    /// Hover in.
    /// This method is called
    /// from event system component.
    /// </summary>
    public void HoverIn() {
        bool audioPlayed = false;
        _cursorIn = true;

        // update item image anim when this assignable has an item in.
        if ( ! empty ) {
            _audio.PlaySound(0);
            audioPlayed = true;
            itemImageAnim.SetBool( "Hover", true );
        }

        // display item temporally to tell the player than the item can be dropped here.
        if ( DragHandler.itemHandled != null && DragHandler.itemHandled.type == ItemData.Type.basic ) {
            if ( ! audioPlayed ) {
                _audio.PlaySound(0);
            }
            _anim.SetBool( "Hover", true );
            itemImage.sprite = DragHandler.itemHandled.sprite;
        }
    }

    /// <summary>
    /// Assign item to this
    /// assignable.
    /// </summary>
    /// <param name="slotType">Slot.Type - slot type from where the item comes from</param>
    public void AssigItem( Slot.Type slotType ) {
        // assign item.
        if ( slotType == Slot.Type.item ) {
            if ( DragHandler.itemHandled.type == ItemData.Type.basic ) {
                _audio.PlaySound(1);
                _anim.SetBool( "ItemAssigned", true );

                assignableData.itemData = DragHandler.itemHandled;
                itemImage.sprite = DragHandler.itemHandled.sprite;
                
                empty = false;
            }
        }

        // TODO: Assign weapon logic.
    }

    /// <summary>
    /// Remove item assigned to this
    /// assignable.
    /// </summary>
    public void RemoveItem() {
        _audio.PlaySound(1);
        _anim.SetBool( "ItemAssigned", false );

        assignableData.itemData = null;
        itemImage.sprite = defaultSprite;
        empty = true;
    }

    /// <summary>
    /// Hover out.
    /// This method is called
    /// from event system component.
    /// </summary>
    public void HoverOut() {
        _cursorIn = false;

        // update background if this assginable has no item assigned.
        if ( empty ) {

            // display item temporally to tell the player than the item can be dropped here.
            if ( DragHandler.itemHandled != null && DragHandler.itemHandled.type == ItemData.Type.basic ) {
                _anim.SetBool( "Hover", false );
                itemImage.sprite = defaultSprite;
            }
        }

        // update item image anim when this assignable has an item in.
        if ( ! empty ) {
            itemImageAnim.SetBool( "Hover", false );

            if ( DragHandler.itemHandled != null && DragHandler.itemHandled.type == ItemData.Type.basic ) {
                _anim.SetBool( "Hove", false );
            }
        }
    }

    /// <summary>
    /// Set up current assignated
    /// item if there is one.
    /// </summary>
    private void SetUpCurrentAssignated() {
        _anim.SetBool( "ItemAssigned", true );

        itemImage.sprite = assignableData.itemData.sprite;
                
        empty = false;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        if ( _anim == null ) {
            _anim = GetComponent<Animator>();
        }

        // get audio component reference.
        if ( _audio == null ) {
            _audio = GetComponent<AudioComponent>();
        }

        // debug.
        if ( assignableData.resetAtInit ) {
            assignableData.Reset();
        }

        // set up item assigned if required.
        if ( assignableData.itemData != null ) {
            SetUpCurrentAssignated();
        }
    }
}
