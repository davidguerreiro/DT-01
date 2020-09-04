using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCristal : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter( Collision other ) {

        Debug.Log( other.gameObject.tag );

        if ( other.gameObject.tag == "PlayerProjectile" ) {
            Debug.Log( "Collision ok" );
        } 
    }
}
