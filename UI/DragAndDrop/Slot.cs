using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
    public enum Type {
        item,
        weapon,
    };
    public Slot.Type slotType = new Slot.Type();                    // Slot type. Defines which kind of object can be dropped and assigned to this element.
    private Assignable _assignable;                                 // Assignable element - used to assign the dragged object droped in this slot.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    /// <summary>
    /// Draggable object dropped
    /// in this slot logic handler
    /// method.
    /// </summary>
    public void OnDrop( PointerEventData eventData ) {
        Debug.Log( eventData );
        _assignable.AssigItem( slotType );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        if ( _assignable == null ) {
            switch ( slotType ) {                   // TODO: Add get assignable for weapons.
                case Slot.Type.item:
                    _assignable = GetComponent<Assignable>();
                    break;
                default:
                    break;
            }
        }
    }
}
