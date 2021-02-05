using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCrystal : MonoBehaviour {
    [Header("Status")]
    public bool interactable;                                   // Whether this crystal is interactble or not.
    public bool completeEnabled;                                // If true, this crystal has already been enabled as a switch.

    [Header("Additional Elements")]
    public RotateAround[] rotateElements;                       // Roteate around decorative elements.
    public GravityPlatform[] gravityPlatforms;                  // Gravity platforms attached to this gravity crystal.

    [Header("Settings")]
    public float activationHits;                                // How many hits this gravity cristal needs to release graviti energy.
    public float rotationAnimDuration;                          // How long the anim rotation is running.
    public float minRotSpeed;                                   // Minumun rotate elements rotation speed.
    public float maxRotSpeed;                                   // Maximun rotate elements rotation speed.
    public bool triggersCinematic;                              // If true, will trigger a cinematic when enabled.

    [Header("Components")]
    public MeshCollider childCollider;                          // Child collider mesh component reference.
    public ParticleSystem distorsion;                           // Distorsion particle effect.
    public ParticleSystem particleHit;                          // Hit particle effect.
    public ParticleSystem particleStatic;                       // Static particle system.

    private Animator _anim;                                     // Animator component reference.
    private AudioComponent _audio;                              // Audio component refernece.
    private Coroutine _nebuloseAnimRoutine;                     // Nebulose rotation coroutine reference.
    private CinematicTrigger _cinematicTrigger;                 // Cinematic trigger class component reference.

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    /// <summary>
    /// Set rotation elements speed.
    /// </summary>
    private void SetRotationalElementsSpeed() {
        foreach ( RotateAround rotElement in rotateElements ) {
            rotElement.speed = minRotSpeed;
        }
    }

    /// <sumamry>
    /// Hit method.
    /// Called every time the gravity
    /// crystal is hit by player's projectile.
    /// </summary>
    private void Hit() {
        if ( interactable ) {
            if ( activationHits > 0 ) {
                _anim.SetTrigger( "Hit" );
                distorsion.Play();
                particleHit.Play();
                _audio.PlaySound(0);
                activationHits--;
            } else {
                // call enabled method here.
                Enabled();
            }
        }
    }

    /// <summary>
    /// Enabled method.
    /// </summary>
    private void Enabled() {
        interactable = false;
        completeEnabled = true;

        _anim.SetBool( "Enabled", true );
        distorsion.Play();
        particleHit.Play();
        particleStatic.Play();
        _audio.PlaySound(1);

        if ( _nebuloseAnimRoutine == null ) {
            _nebuloseAnimRoutine = StartCoroutine( PlayNebuloseAnimation() );
        }
        
        if ( triggersCinematic ) {
            _cinematicTrigger.TriggerCinamatic();
        } else {
            foreach ( GravityPlatform gravityPlatform in gravityPlatforms ) {
                gravityPlatform.MovePlatformToEndPoint();
            }
        }
    }

    /// <summary>
    /// Rotate nebulose animation.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlayNebuloseAnimation() {
        // accelerate.
        for ( int i = (int) minRotSpeed; i <= maxRotSpeed; i++ ) {
            foreach ( RotateAround rotateElement in rotateElements ) {
                rotateElement.speed++;
            }

            i++;
            yield return new WaitForSeconds( .01f );
        }

        yield return new WaitForSeconds( rotationAnimDuration );

        // decelerate.
        for ( int i = (int) minRotSpeed; i <= maxRotSpeed; i++ ) {
            foreach ( RotateAround rotateElement in rotateElements ) {
                rotateElement.speed--;
            }

            i++;
            yield return new WaitForSeconds( .01f );
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter( Collision other ) {
        if ( interactable && other.gameObject.tag == "PlayerProjectile" ) {
            Hit();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component.
        _anim = GetComponent<Animator>();

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();

        if ( interactable ) {
            _anim.SetBool( "Interactable", true );
        }

        if ( triggersCinematic ) {
            _cinematicTrigger = GetComponent<CinematicTrigger>();
        }

        // set initial rotation for elements.
        SetRotationalElementsSpeed();
    }
}
