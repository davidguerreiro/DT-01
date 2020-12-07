using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : Bullet {
    public bool readyToShoot;                                   // When true, the charged bullet is ready to be shot at target.

    [Header("Settings")]
    public Vector3 maxSize;                                     // Size to reach when the charged bullet is being charged.
    public float speed;                                         // Charging speed.

    [Header("Components")]
    public ParticleSystem coreParticles;                        // Core partuicles Particle system refernece.
    public ParticleSystem shootParticles;                       // Shoot particles.
    public GameObject impactEffect;                             // Impact effect displayed when the charged bullet collides and explodes.

    private AudioComponent _audioComponent;                     // Audio component reference.
    private Animator _anim;                                     // Animator component reference.
    private Coroutine _chargedCoroutine;                        // Charging coroutone reference.


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Charge shoot.
    /// </summary>
    public void ChargeShoot() {
        if ( _chargedCoroutine == null ) {
            _chargedCoroutine = StartCoroutine( ChargeShootRoutine() );
        } 
    }

    /// <summary>
    /// Charge shoot coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ChargeShootRoutine() {
        bool xReady = false;
        bool yReady = false;
        bool zReady = false;
        Vector3 growing = transform.localScale;

        _audioComponent.PlaySound(0);
        coreParticles.Play();

        while ( ! xReady || ! yReady || ! zReady ) {
            float toGrown = speed * Time.deltaTime;
            growing = new Vector3( growing.x + toGrown, growing.y + toGrown, growing.z + toGrown );

            if ( growing.x >= maxSize.x ) {
                xReady = true;
            }

            if ( growing.y >= maxSize.y ) {
                yReady = true;
            }

            if ( growing.z >= maxSize.z ) {
                zReady = true;
            }

            transform.localScale = growing;
            yield return new WaitForFixedUpdate();
        }
        
        // fix any floating error after animation.
        transform.localScale = new Vector3( maxSize.x, maxSize.y, maxSize.z );

        // charged shoot ready to be shoot.
        _anim.SetBool( "Completed", true );
        _audioComponent.PlaySound(1);

        readyToShoot = true;
        _chargedCoroutine = null;

        Debug.Log( "ready to shoot" );
    }

    /// <summary>
    /// Stop charging routine.
    /// </summary>
    public void StopCharging() {
        if ( _chargedCoroutine != null ) {
            StopCoroutine( _chargedCoroutine );
            _chargedCoroutine = null;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audioComponent = GetComponent<AudioComponent>();

        // get animtor component reference.
        _anim = GetComponent<Animator>();
    }
}
