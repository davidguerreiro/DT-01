using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assignable : MonoBehaviour {
    public bool empty;                                      // Whether this assignable slot is ready to get an item assigned.
    
    [Header("Assignable data")]
    public ItemAssignable assignableData;                   // Item assignable data.

    [Header("Components")]
    public Image background;                                // Assignable background image component.
    public Text text;                                       // Assignable text component.
    public Image itemImage;                                 // Item image displayed in this assignable component.
    public Sprite defaultSprite;                            // Default sprite.
    public Sprite inUseSprite;                              // Current in-use sprite - used to keep a reference of current item if the user tries to assign new item but the slot is not empty.

    [Header("Defaults")]
    public Color defaultBackgroundColor;                    // Default color for background. Used to reset element.
    public Color defaultTextColor;                          // Default color for text. Used to reset element.  

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
        _cursorIn = true;

        // display item temporally to tell the player than the item can be dropped here.
        if ( DragHandler.itemHandled != null && DragHandler.itemHandled.type == ItemData.Type.basic ) {
            _audio.PlaySound(0);
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
                inUseSprite = DragHandler.itemHandled.sprite;
                
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
        _anim.SetBool( "Hover", false );

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

            if ( DragHandler.itemHandled != null && DragHandler.itemHandled.type == ItemData.Type.basic ) {
                itemImage.sprite = inUseSprite;
            }
        }
    }

    /// <summary>
    /// Set up current assignated
    /// item if there is one.
    /// </summary>
    private void SetUpCurrentAssignated() {

        Debug.Log( "called" );
        itemImage.sprite = assignableData.itemData.sprite;
        inUseSprite = itemImage.sprite;
        
        background.color = defaultBackgroundColor;
        // text.color = defaultTextColor;

        _anim.SetBool( "ItemAssigned", true );
                
        empty = false;
    }
    
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable() {
        Debug.Log( "on enable" );
        // set up item assigned if required.
        if ( assignableData.itemData != null ) {
            SetUpCurrentAssignated();
        }
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
