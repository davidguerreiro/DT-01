﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    [Header("Options")]
    public bool isFloating;                                   // Wheter the platform is some kind of floating.
    public bool isMoving;                                     // Wheter the platform is moving.

    [Header("Floating Settings")]
    public float floatingSpeed;                               // Floating speed.
    public float minHeight;                                   // Minimun height for floating platform animation.
    public float maxHeight;                                   // Maximun height for floating platform animation.
    public float staticWait;                                  // Time the platform stops before continue floating animation.

    [Header("Moving Settings")]
    public float movingSpeed;                                 // Platform moving speed.
    public bool loop = false;                                 // Whether is moving in loop.
    public bool singleMovement = false;                       // Whether it moves from point A to B only and never returns.
    public Transform pointA;                                  // Initial lerp position.
    public Transform pointB;                                  // End lerp position.
    
    private float _initialHeight;                             // Initial height reference.
    private Coroutine _floatingCoroutine;                     // Floating coroutine reference.
    private Coroutine _movingCoroutine;                       // Moving coroutine reference.
    private Vector3 _minimunPosition;                         // Lowest position for floating animation.
    private Vector3 _maxPosition;                             // Highest position for floating animation.
    private bool _floatingFlag = false;                       // Floating coroutine control flag.
    private bool _movementFlag = false;                       // Movement coroutine control flag.

    // Start is called before the first frame update
    void Start() {
        Init();

        // move platform once.
        if ( isMoving && ! loop ) {
            _movingCoroutine = StartCoroutine( "Moving" );
        }
    }

    // Update is called once per frame
    void Update() {
        
        // start floating if required.
        if ( isFloating && ! _floatingFlag ) {
            _floatingCoroutine = StartCoroutine( "Floating" );
        }

        // start moving if required.
        if ( isMoving && loop && ! _movementFlag ) {
            _movingCoroutine = StartCoroutine( "Moving" );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        _initialHeight = transform.position.y;

        if ( isFloating ) {
            // transform.position = new Vector3( transform.position.x, transform.position.y - minHeight, transform.position.z );

            _minimunPosition = new Vector3( transform.position.x, transform.position.y - minHeight, transform.position.z );
            _maxPosition = new Vector3( transform.position.x, transform.position.y + maxHeight, transform.position.z );
        }

        _floatingCoroutine = null;
        _movingCoroutine = null;
    }

    /// <summary>
    /// Floating coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator Floating() {
        _floatingFlag = true;
        
        Vector3 targetPosition = _maxPosition;
        float moveTime = 0f;

        // static time, no movement for the platform.
        yield return new WaitForSeconds( staticWait );

        // up movement.
        while ( Vector3.Distance( targetPosition, transform.position ) > Mathf.Epsilon ) {

            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp( _minimunPosition, targetPosition, moveTime / floatingSpeed );
            yield return null;
        }

        // static time, no movement for the platform.
        yield return new WaitForSeconds( staticWait );

        // down time.
        targetPosition = _minimunPosition;
        moveTime = 0f;

        while ( Vector3.Distance( targetPosition, transform.position ) > Mathf.Epsilon ) {

            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp( _maxPosition, targetPosition, moveTime / floatingSpeed );
            yield return null;
        }
        
        _floatingFlag = false;
    }

    /// <summary>
    /// Moving platform coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator Moving() {

        _movementFlag = true;
        Vector3 targetPosition = pointB.transform.position;
        float moveTime = 0f;

        // static time, no movement for the platform.
        yield return new WaitForSeconds( staticWait );

        // forward movement.
        while ( Vector3.Distance( targetPosition, transform.position ) > Mathf.Epsilon ) {

            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp( pointA.transform.position, targetPosition, moveTime / movingSpeed );

            yield return null;
        }

        if ( ! singleMovement ) {

            // static time, no movement for the platform.
            yield return new WaitForSeconds( staticWait );

            targetPosition = pointA.transform.position;
            moveTime = 0f;

            // back movement
            while ( Vector3.Distance( targetPosition, transform.position ) > Mathf.Epsilon ) {
                
                moveTime += Time.deltaTime;
                transform.position = Vector3.Lerp( pointB.transform.position, targetPosition, moveTime / movingSpeed );

                yield return null;
            }
        }

        _movementFlag = false;
    }

    /// <summary>
    /// Joint player to the platform, so the player
    /// moves along within the platform.
    /// </summary>
    /// <param name="player">GameObject - player gameobject reference</param>
    private void JointPlayerToPlatform( GameObject player ) {

        // check if player grounded on the platform.
        FPSInput playerInput = player.GetComponent<FPSInput>();

        if ( playerInput != null && playerInput.grounded ) {
            player.transform.parent = this.gameObject.transform.parent;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter( Collision other ) {
        
        if ( other.gameObject.tag == "Player" ) {
            JointPlayerToPlatform( other.gameObject );
        } 
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit( Collision other ) {

        if ( other.gameObject.tag == "Player" ) {
            other.gameObject.transform.parent = null;
        }
    }
}