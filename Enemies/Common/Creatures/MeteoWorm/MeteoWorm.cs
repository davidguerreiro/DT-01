﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoWorm : Enemy {

    public Animator anim;                             // Animator component reference.
    private AudioComponent _audio;                    // Audio component reference.

    [Header("Testing")]
    public Transform destinationTest;

    // Start is called before the first frame update
    void Start() {
        Init();

        if ( destinationTest != null ) {
            Move( new Vector3( destinationTest.position.x, transform.position.y, destinationTest.position.z ) );
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {
        if ( other.gameObject.tag == "PlayerProjectile" ) {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            GetDamage( bullet.damage );
        }
    }

    /// <summary>
    /// Get damage.
    /// </summary>
    /// <param name="externalImpactValue">float - damage value caused external attacker, usually the player.</param>
    public override void GetDamage( float externalImpactValue ) {
        base.GetDamage( externalImpactValue );

        // play damage sound.
        if ( _audio != null ) {
            _audio.PlaySound( 0 );
        }
    }

    /// <summary>
    /// Die method.
    /// </summary>
    public override IEnumerator Die() {
        StartCoroutine( base.Die() );
        
        // play death animation.
        anim.SetBool( "Die", true );
        yield return new WaitForSeconds( timeToDissapear );

        // remove enemy from the scene.
        RemoveEnemy();
    }

    /// <summary>
    /// Move enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to move</param>
    public new void Move( Vector3 destination ) {
        if ( moveCoroutine == null ) {
            moveCoroutine = base.StartCoroutine( Move( destination, anim, "IsMoving" ) );
        }
    }

    /// <summary>
    /// Revove enemy from the scene after
    /// dying.
    /// </summary>
    private void RemoveEnemy() {

        // TODO: Remove enemy with soft transparent animation.
        if ( parentReference != null ) {
            Destroy( parentReference );
        } else {
            Destroy( this.gameObject );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public override void Init() {
        base.Init();

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
