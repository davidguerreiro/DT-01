﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalSplinter : MonoBehaviour {
    [Header( "Settings" )]
    public float secondsDisplayed;                         // Seconds before the splinter dissapears from game scene.

    [Header( "Physics" )]
    public Vector3 minForce;                               // Minimun values for splinter force algorythm.
    public Vector3 maxForce;                               // Max values for splinter force algorythm.

    private Animator _animator;                            // Animator component reference.
    private Rigidbody _rigi;                               // Rigibody component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Fade out splinter from
    /// scene.
    /// </summary>
    private void FadeOutSplinter() {
        _animator.SetBool( "FadeOut", true );

        Destroy( this.gameObject, 2f );
    }

    /// <summary>
    /// Explode, usually used when 
    /// cristals are hit by the player.
    /// </summary>
    private void ApplyForce() {

        // generate forces.
        float xForce = Random.Range( minForce.x, maxForce.x );
        float yForce = Random.Range( minForce.y, maxForce.y );
        float zForce = Random.Range( minForce.z, maxForce.z );

        // apply forces to spliter.
        _rigi.AddForce( xForce, yForce, zForce );
    }

    /// <summary>
    /// Split splinter from cristal
    /// source.
    /// </summary>
    public void SplitFromCrystal() {
        ApplyForce();

        Invoke( "FadeOutSplinter", secondsDisplayed );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _animator = GetComponent<Animator>();

        // get rigibody component reference.
        _rigi = GetComponent<Rigidbody>();
    }
}
