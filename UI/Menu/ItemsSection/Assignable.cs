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

    private Animator _anim;                                 // Anim component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Hover in.
    /// This method is called
    /// from event system component.
    /// </summary>
    public void HoverIn() {

        // update item image anim when this assignable has an item in.
        if ( ! empty ) {
            itemImageAnim.SetBool( "Hover", true );
        }
    }

    /// <summary>
    /// Hover out.
    /// This method is called
    /// from event system component.
    /// </summary>
    public void HoverOut() {

        // update item image anim when this assignable has an item in.
        if ( ! empty ) {
            itemImageAnim.SetBool( "Hover", false );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        if ( _anim != null ) {
            _anim = GetComponent<Animator>();
        }
    }
}
