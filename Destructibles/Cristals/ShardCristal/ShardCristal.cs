using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCristal : Cristal {
    private SphereCollider sphereCollider;
    
    // Start is called before the first frame update
    void Start() {
        Init();
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

        // check if the cristal is being hit by a player projectile.
        if ( other.gameObject.tag == "PlayerProjectile" ) {
            Hit( other.gameObject.GetComponent<Bullet>().damage );
        }
    }

    /// <summary>
    /// Cristal hit by player actions.
    /// </summary>
    /// <param name="damage">float - damage received by hit impact.</param>
    public override void Hit( float damage ) {
        base.Hit( damage );

        if ( resistance > 0 ) {
            base._animator.SetTrigger( "hit" );

            // display hit particles here.
            
        } else {
            // destroy here.
            Destroyed();
        }
    }

    /// <summary>
    /// Cristal destroyed.
    /// </summary>
    public override void Destroyed() {
        base.Destroyed();

        // disable collider.
        Destroy( sphereCollider );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get sphere collider reference.
        sphereCollider = GetComponent<SphereCollider>();
    }


}
