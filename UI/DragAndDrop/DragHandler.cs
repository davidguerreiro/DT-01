using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [HideInInspector]
    public static Item itemHandled;                                 // Item handled by the user.
    private ItemBox _itemBox;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }


    /// <summary>
    /// Begin drag event handler.
    /// </summary>
    /// <param name="eventData">PointerEventData - Cursir pointer event data</param>
    public void OnBeginDrag( PointerEventData eventData ) {
        Debug.Log( eventData );
    }

    /// <summary>
    /// Logic run during the time
    /// the user handles the dragged 
    /// element.
    /// </summary>
    /// <param name="eventData">PointerEventData Cursor pointer event data</param>
    public void OnDrag( PointerEventData eventData ) {

    }

    /// <summary>
    /// End drag event handler.
    /// </summary>
    /// <param name="eventData">PointerEventData Cursor pointer event data</param>
    public void OnEndDrag( PointerEventData eventData ) {

    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        // get itembox component reference.
        if ( _itemBox == null ) {
            _itemBox = GetComponent<ItemBox>();
        }
    }
}
