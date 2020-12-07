using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : Bullet {
    public bool readyToShoot;                                   // When true, the charged bullet is ready to be shot at target.

    [Header("Settings")]
    public Vector3 maxSize;                                     // Size to reach when the charged bullet is being charged.
    public float speed;                                         // Charging speed.
    public float lightSpeed;                                    // Switch on speed light animation.
    public float maxLightIntensity;                             // Max light intensity.
    public Color initColor;                                     // Init original color.

    [Header("Components")]
    public ParticleSystem plasmaProyectile;                     // Plasma proyectile gameObject reference.
    public ParticleSystem coreParticles;                        // Core particles Particle system refernece.
    public ParticleSystem shootParticles;                       // Shoot particles.
    public Light topLight;                                      // Top light component reference.
    public Light frontLight;                                    // Front light component reference.
    public GameObject impactEffect;                             // Impact effect displayed when the charged bullet collides and explodes.

    [HideInInspector]
    public FPSInput playerInput;                                // Player input class component reference.

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
        plasmaProyectile.gameObject.SetActive( true );
        plasmaProyectile.Play();

        Vector3 growing = plasmaProyectile.gameObject.transform.localScale;

        _audioComponent.PlaySound(0);
        coreParticles.gameObject.SetActive( true );
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

            if ( topLight.intensity < maxLightIntensity ) {
                topLight.intensity += lightSpeed * Time.deltaTime;
            }

            if ( frontLight.intensity < maxLightIntensity ) {
                frontLight.intensity += lightSpeed * Time.deltaTime;
            }

            plasmaProyectile.gameObject.transform.localScale = growing;
            yield return new WaitForFixedUpdate();
        }
        
        // fix any floating error after animation.
        plasmaProyectile.gameObject.transform.localScale = new Vector3( maxSize.x, maxSize.y, maxSize.z );
        topLight.intensity = maxLightIntensity;
        frontLight.intensity = maxLightIntensity;

        // charged shoot ready to be shoot.
        _anim.SetBool( "Completed", true );
        _audioComponent.PlaySound(1);

        readyToShoot = true;
        _chargedCoroutine = null;
    }

    /// <summary>
    /// Play shoot animation and
    /// sound.
    /// </summary>
    public void PlayShootAnim() {
        shootParticles.gameObject.SetActive( true );
        _audioComponent.PlaySound(2);
    }

    /// <summary>
    /// Reset charged bullet.
    /// </summary>
    public void ResetChargedBullet() {
        var coreParticlesMain = coreParticles.main;
        coreParticlesMain.startColor = initColor;
        
        coreParticles.Stop();
        coreParticles.gameObject.SetActive( false );
    
        shootParticles.Stop();
        shootParticles.gameObject.SetActive( false );

        topLight.intensity = 0;
        frontLight.intensity = 0;

        _anim.SetBool( "Completed", false );

        readyToShoot = false;
        _chargedCoroutine = null;

        var plasmaProyectileMain = plasmaProyectile.main;
        plasmaProyectileMain.startColor = initColor;
        plasmaProyectile.gameObject.transform.localScale = Vector3.zero;
        plasmaProyectile.Stop();
        plasmaProyectile.gameObject.SetActive( false );
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
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {

        // display bullet impact particle effect if neccesary.
        ShootingImpact impact = other.gameObject.GetComponent<ShootingImpact>();

        if ( impact != null ) {
            impact.DisplayImpact( transform.position );
        }
        // old effect.
        /// Instantiate( impactParticles, transform.position, Quaternion.identity );
        
        base.RestoreBullet();
        
        // TODO: Display explosion here.
        ResetChargedBullet();
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other) {
        base.RestoreBullet();
        ResetChargedBullet();
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit(Collision other) {
        base.RestoreBullet();
        ResetChargedBullet();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public override void Init() {
        
        // init parent.
        base.Init();

        // get audio component reference.
        _audioComponent = GetComponent<AudioComponent>();

        // get animtor component reference.
        _anim = GetComponent<Animator>();
    }
}
