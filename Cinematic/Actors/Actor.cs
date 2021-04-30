using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

    [Header("Settings")]
    public bool animated = true;                                // Flag to control if this actor uses animations.
    public float moveSpeed = 1f;                                // Movement speed value.
    public float animMoveSpeed = 1f;                            // Animation movement speed value.
    public GameObject[] interactables;                          // Scene Interactables.

    [Header("Status")]
    public string state = "idle";                               // Current machine state for animations.
    public bool isMoving = false;                               // Flag to control movement.
    public bool[] events = new bool[15];                        // Actor events array

    [HideInInspector]
    public Coroutine moveCoroutine;                             // Move coroutine reference.

    protected Animator _moveAnim;                                   // Animator component reference.
    protected Rigidbody _rigi;                                  // Rigibody component reference.
    protected AudioComponent _audio;                            // Audio component reference.

    public enum ActionType {
        bolean,
        trigger,
        integer,
        floating,
    };

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    // Update is called once per frame.
    void Update() {
        
        if ( animated && ! GameManager.instance.isPaused) {
            CheckAnimationStatus();
        }
    }

    /// <summary>
    /// Check base animation status.
    /// </summary>
    private void CheckAnimationStatus() {

        // check for walking animation.
        if ( state == "walking" ) {
            _moveAnim.SetBool( "walking", true );
        } else {
            _moveAnim.SetBool( "walking", false );
        }

    }

    /// <summary>
    /// Move actor.
    /// </summary>
    /// <param name="destination">Vector3 - position where the actor is going to move</param>
    /// <param name="extraSpeed">float - any extra speed to apply to this movement call. Default to 1f</param>
    /// <param name="newState">State - New state to apply to actor when the movement coroutine finishes. Default to null.</param>
    public virtual IEnumerator Move( Vector3 destination, float extraSpeed = 1f, string newState = null ) {
        isMoving = true;
        state = "walking";

        // TODO: Replace by rotate method.
        transform.LookAt( destination );
        yield return new WaitForSeconds( .1f );

        _moveAnim.SetFloat( "AnimSpeed", animMoveSpeed );

        float remainingDistance = ( transform.position - destination ).sqrMagnitude;

        // move using rigibody and physics engine.
        while ( remainingDistance > 0.1f ) {
            Vector3 newPosition = Vector3.MoveTowards( _rigi.position, destination, ( moveSpeed * extraSpeed ) * Time.deltaTime );
            _rigi.MovePosition( newPosition );

            // ensure actor is looking at the new destination.
            transform.LookAt( destination );

            yield return new WaitForFixedUpdate();

            // update destination if required and remaining distance.
            remainingDistance = ( transform.position - destination ).sqrMagnitude;    
        }

        if ( newState != null ) {
            state = newState;
        } else {
            state = "idle";
        }

        _moveAnim.SetFloat( "AnimSpeed", 1f );

        isMoving = false;
        moveCoroutine = null;
    }

    /// <summary>
    /// Stop movement.
    /// </summary>
    /// <param name="newState">string - set new state after stop moving. Null by default</param>
    public void StopMoving( string newState = null ) {
        if ( isMoving ) {
            isMoving = false;
        }

        if ( moveCoroutine != null ) {
            StopCoroutine( moveCoroutine );
            moveCoroutine = null;
        }

        if ( newState != null ) {
            state = newState;
        }
    }

    /// <summary>
    /// Set event in events array.
    /// </summary>
    public void SetEvent() {
        for (int i = 0; i < events.Length; i++) {
            if ( !events[i] ) {
                events[i] = true;
                break;
            }
        }
    }

    /// <summary>
    /// Set event in events array.
    /// </summary>
    /// <parma name="key">int - array key whose event must be set.</param>
    public void SetEventByKey(int key) {
        events[key] = true;
    }

    /// <summary>
    /// Restore events.
    /// </summary>
    public void RestoreEvents() {
        for (int i = 0; i < events.Length; i++) {
            events[i] = false;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference
        if ( animated ) {
            _moveAnim = GetComponent<Animator>();
        }

        // get rigibody component reference.
        _rigi = GetComponent<Rigidbody>();
    }
}
