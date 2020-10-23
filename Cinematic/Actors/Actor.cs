using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    [Header("Settings")]
    public bool animated = true;
    public float moveSpeed = 1f;                            // Movement speed value.
    public GameObject[] interactables;                      // Scene Interactables.

    [Header("Status")]
    public string state = "idle";                           // Current machine state for animations.
    public bool isMoving = false;                           // Flag to control movement.

    [HideInInspector]
    public Coroutine moveCoroutine;                         // Move coroutine reference.

    protected Animator _anim;                                 // Animator component reference.
    protected Rigidbody _rigi;                                // Rigibody component reference.
    protected AudioComponent _audio;                          // Audio component reference.

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
        
    }

    /// <summary>
    /// Move actor.
    /// </summary>
    /// <param name="destination">Vector3 - position where the actor is going to move</param>
    /// <param name="extraSpeed">float - any extra speed to apply to this movement call. Default to 1f</param>
    /// <param name="newState">State - New state to apply to actor when the movement coroutine finishes. Default to null.</param>
    public virtual IEnumerator Move( Vector3 destination, float extraSpeed = 1f, string newState = null ) {
        isMoving = true;

        // TODO: Replace by rotate method.
        transform.LookAt( destination );
        yield return new WaitForSeconds( .1f );

        
        float remainingDistance = ( transform.position - destination ).sqrMagnitude;

        // move using rigibody and physics engine.
        while ( remainingDistance > 0.1f ) {

            Vector3 newPosition = Vector3.MoveTowards( _rigi.position, destination, ( moveSpeed * extraSpeed ) * Time.deltaTime );
            _rigi.MovePosition( newPosition );

            // ensure enemy is looking at the new destination.
            transform.LookAt( destination );

            yield return new WaitForFixedUpdate();

            // update destination if required and remaining distance.
            remainingDistance = ( transform.position - destination ).sqrMagnitude;    
        }

        if ( newState != null ) {
            state = newState;
        }

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
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference
        if ( animated ) {
            _anim = GetComponent<Animator>();
        }

        // get rigibody component reference.
        _rigi = GetComponent<Rigidbody>();
    }
}
