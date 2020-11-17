using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCrystal : MonoBehaviour {
    public bool interactable;                                   // Whether this crystal is interactble or not.
    public bool completeEnabled;                                // If true, this crystal has already been enabled as a switch.

    private Animator _anim;                                     // Animator component refernece.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component.
        _anim = GetComponent<Animator>();

        if ( interactable ) {
            _anim.SetBool( "Interactable", true );
        }
    }
}
