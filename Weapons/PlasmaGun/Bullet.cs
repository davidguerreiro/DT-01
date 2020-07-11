﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Vector3 _originalPosition;                               // Original bullet position. Used to restore bullet to weapoin shooting origin after the bullet is destroyed.                  
    private Vector3 _destination;                                    // Where the bullet is shot towards.
    private float _speed;                                            // Movement speed. Defined by weapon.
    private bool _shooted = false;                                   // When true, bullet can move towards destination.

    void Awake() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
        if ( _shooted ) {
            SentBullet();
        }
        
    }

    /// <summary>
    /// Set up bullet parameters.
    /// </summary>
    /// <parma name="destination">Vector3 - bullet destination - it is the point where the player has shot.</param>
    /// <param name="speed">float - movement speed</param>
    public void ShootBullet( Vector3 destination, float speed ) {

        this._destination = destination;
        this._speed = speed;

        _shooted = true;
    }


    /// <summary>
    /// Move bullet from the 
    /// weapon from where is shot
    /// to target.
    /// </summary>
    private void SentBullet() {
        transform.position = Vector3.MoveTowards( transform.position, _destination, _speed * Time.deltaTime );
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        Debug.Log( LayerMask.LayerToName( other.gameObject.layer) );
        _shooted = false;

        transform.localPosition = _originalPosition;
        gameObject.SetActive( false );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // set original position attribute.
        _originalPosition = transform.localPosition;
    }
}