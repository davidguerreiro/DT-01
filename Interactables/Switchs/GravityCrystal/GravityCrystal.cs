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
    public float minRotSpeed;                                   // Minumun rotate elements rotation speed.
    public float maxRotSpeed;                                   // Maximun rotate elements rotation speed.

    [Header("Components")]
    public MeshCollider childCollider;                          // Child collider mesh component reference.

    private Animator _anim;                                     // Animator component reference.
    private AudioComponent _audio;                              // Audio component refernece.

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
