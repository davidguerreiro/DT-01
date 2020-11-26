using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCrystal : MonoBehaviour {
    [Header("Status")]
    public bool interactable;                                   // Whether this crystal is interactble or not.
    public bool completeEnabled;                                // If true, this crystal has already been enabled as a switch.

    [Header("Additional Elements")]
    public RotateAround[] rotateElements;                       // Roteate around decorative elements.

    [Header("Settings")]
    public float activationHits;                                // How many hits this gravity cristal needs to release graviti energy.
    public float rotationAnimDuration;                          // How long the anim rotation is running.
    public float minRotSpeed;                                   // Minumun rotate elements rotation speed.
    public float maxRotSpeed;                                   // Maximun rotate elements rotation speed.

    [Header("Components")]
    public MeshCollider childCollider;                          // Child collider mesh component reference.
    public ParticleSystem distorsion;                           // Distorsio particle effect.

    private Animator _anim;                                     // Animator component reference.
    private AudioComponent _audio;                              // Audio component refernece.
    private Coroutine _nebuloseAnimRoutine;                     // Nebulose rotation coroutine reference.

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
        _audio.PlaySound(1);

        if ( _nebuloseAnimRoutine == null ) {
            _nebuloseAnimRoutine = StartCoroutine( PlayNebuloseAnimation() );
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
        Debug.Log( other.gameObject.tag );
        if ( interactable && other.gameObject.tag == "PlayerProjectile" ) {
            Debug.Log( "called" );
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

        // set initial rotation for elements.
        SetRotationalElementsSpeed();
    }
}
