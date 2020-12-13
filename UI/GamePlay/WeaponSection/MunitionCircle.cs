using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MunitionCircle : MonoBehaviour {

    public PlasmaGun data;                               // Data source.

    private Image _bar;                                  // Filled image bar component reference.
    private Animator _anim;                              // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if ( ! GameManager.instance.isPaused ) {
            ListenForHeated();
        }
    }

    /// <summary>
    /// Update munition circle.
    /// </summary>
    /// TODO: Check where recharge method is and move to plasma gun.

    /// <summary>
    /// Listen for heated.
    /// </summary>
    public void ListenForHeated() {
        if ( data.IsBelowThreshold() ) {
            _anim.SetBool( "Heated", true );
        } else {
            _anim.SetBool( "Heated", false );
        }
    }

    

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get image component.
        _bar = GetComponent<Image>();

        // get animator component.
        _anim = GetComponent<Animator>();
    }
}
