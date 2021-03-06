﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCristal : Cristal {

    [Header( "Models")]
    public GameObject cristalModel;                             // Crystal 3D model gameobject reference.

    [Header( "Components")]
    public ObjectPool splinters;                                 // Spliters object pool - Used to display crystal spliters each time the crystal receives a hit from player.
    public ObjectPool destroyedSplinters;                        // Splinters object pool - Used for destroyed cristal animation.
    public CristalLight cristalLight;                            // Cristal Light class component reference.
    public MeshCollider meshCollider;                            // Mesh collider component reference.

    [Header( "Settings")]
    public int splintersDisplayed;                               // Spliters displayed by each player hit.
    public float secondsToDestroy;                              // How long the cristal gameObject stays alive in the scene before being removed.

    private Animator _animator;                                 // 3D Model animator component reference.
    private Loot _loot;                                         // Loot to drop after the cristal has been destroyed.
    
    // Start is called before the first frame update
    void Start() {
        Init();
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
            
            // display crystal hit animation.
            if ( _animator != null ) {
                _animator.SetFloat( "Speed", 2f );
                _animator.SetTrigger( "Hit" );
            }

            // display hit particles here.
            for ( int i = 0; i < splintersDisplayed; i++ ) {
                GameObject splinter = splinters.SpawnPrefab( splinters.gameObject.transform.localPosition );

                if ( splinter != null ) {
                    StartCoroutine( splinter.GetComponent<CristalSplinter>().SplitFromCrystal() );
                }
            }
        
            
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

        // Transform crystalModelPosition = cristalModel.transform;

        // disable collider.
        Destroy(meshCollider);

        // display destroyed animation.
        StartCoroutine( DestroyedAnimation() );

        // drop loot.
        if ( _loot != null ) {
            _loot.DropLoot();
        }

        // switch light off.
        cristalLight.SwitchOff();

        // destroy this object.
        Destroy( this.gameObject, secondsToDestroy );
    }

    /// <summary>
    /// Crystal destroyed animation.
    /// </summary>
    private IEnumerator DestroyedAnimation() {

        _animator.SetBool( "Destroy", true );

        // display all broken splinters.
        for ( int i = 0; i < splinters.poolSize; i++ ) {
            GameObject splinter = splinters.SpawnPrefab( splinters.gameObject.transform.localPosition );

            if ( splinter != null ) {
                StartCoroutine( splinter.GetComponent<CristalSplinter>().SplitFromCrystal() );
            }
        }

        yield return new WaitForSeconds( .5f );

        // disable 3D models
        cristalModel.SetActive( false );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public override void Init() {
        base.Init();

        // get 3D model animator componentr.
        if ( cristalModel != null ) {
            _animator = cristalModel.GetComponent<Animator>();
        }

        // get loot class reference.
        _loot = GetComponentInChildren<Loot>();
    }


}
