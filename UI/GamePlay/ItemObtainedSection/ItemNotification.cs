using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotification : MonoBehaviour {
    public bool displayed;                                      // Flag to control if this notification is being used.

    [Header("Components")]
    public FadeElement wrapperAnim;                             // Notification box wrapper animator reference.
    public Image wrapper;                                       // Notification box wrapper image reference.
    public FadeElement quantityAnim;                            // Quantity animator component reference.
    public TextComponent quantityText;                          // Quantity text component refernece.
    public FadeElement itemImageAnim;                           // Item image anim component refernece.
    public Image itemImage;                                     // Item image component reference.
    public FadeElement itemNameAnim;                            // Item image anim component reference.
    public TextComponent itemName;                              // Item name text compoentn reference.

    [Header("Settings")]
    public float gap;                                           // How long it will move up when another notification is displayed.
    public float speed = 10f;                                         // Move to gap speed.
    public float timeDisplayed = 3f;                            // How long it will be displayed ( in seconds ).
    public Color importantItemColor;                            // Color to use in the background in case the player collects an importan item.

    private Vector3 _originalPosition;                          // Original position where the notification will be displayed.
    private Color _originalColor;                                // Original notification's background color.
    private Coroutine _moveRoutine;                              // Move coroutine reference.
    private Coroutine _hideCoroutine;                           // Hide coroutine reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    /// <summary>
    /// Set up item notification data.
    /// </summary>
    /// <param name="item">Item - item instance to display</param>
    /// <parma name="quantity">int - item's quantity obtained.</parma>
    private void SetUpItemData( Item item, int quantity ) {
        quantityText.UpdateContent( quantity.ToString() );
        itemImage.sprite = item.data.sprite;
        itemName.UpdateContent( item.data.itemName_en );

        if ( item.data.type == ItemData.Type.important ) {
            wrapper.color = importantItemColor;
        }
    }

    /// <summary>
    /// Display notification.
    /// </summary>
    /// <param name="item">Item - item instance to display</param>
    /// <parma name="quantity">int - item's quantity obtained.</parma>
    public void DisplayNotification( Item item, int quantity ) {

        if ( _hideCoroutine == null ) {
            // set up item data.
            SetUpItemData( item, quantity );

            // display notification.
            wrapperAnim.FadeIn();
            quantityAnim.FadeIn();
            itemImageAnim.FadeIn();
            itemNameAnim.FadeIn();

            displayed = true;

            _hideCoroutine = StartCoroutine( HideNotification() );
        }
    }

    /// <summary>
    /// Hide notification.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator HideNotification() {
        yield return new WaitForSeconds( timeDisplayed );

        // hide notification.
        wrapperAnim.FadeOut();
        quantityAnim.FadeOut();
        itemImageAnim.FadeOut();
        itemNameAnim.FadeOut();
        yield return new WaitForSeconds( .5f );

    
        ResetNotification();
        displayed = false;
        _hideCoroutine = null;
    }

    /// <summary>
    /// Reset notification.
    /// </summary>
    private void ResetNotification() {
        wrapper.color = _originalColor;
        quantityText.UpdateContent( "+0" );
        itemImage.sprite = null;
        itemName.UpdateContent( "ItemName" );
        transform.position = _originalPosition;
    }

    /// <summary>
    /// Move the notification up.
    /// </summary>
    public void MoveUp() {
        float toMoveY = transform.position.y + gap;

        // another item notificaion enters, stop previos movement.
        if ( _moveRoutine != null ) {
            StopCoroutine( _moveRoutine );
            _moveRoutine = null;
            transform.position = new Vector3( transform.position.x, toMoveY, transform.position.z );
        }

        _moveRoutine = StartCoroutine( MoveUpRoutine( toMoveY ) );
    }

    /// <summary>
    /// Move up routine.
    /// </summary>
    /// <param name="toMoveY">float - where to move.</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator MoveUpRoutine( float toMoveY ) {
        while ( transform.position.y < toMoveY ) {
            transform.position = new Vector3( transform.position.x, transform.position.y + ( speed * Time.deltaTime ), transform.position.z );
            yield return null;
        }

        // fix any float error.
        transform.position = new Vector3( transform.position.x, toMoveY, transform.position.z );
        _moveRoutine = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        
        // get original position.
        _originalPosition = transform.position;

    }
}
