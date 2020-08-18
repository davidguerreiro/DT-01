using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeElement : MonoBehaviour {
    public bool displayed;                                  // Flag to control displayed status.
    private Animator _animator;                             // Animator component reference.

    /**
    * Fade idle / half fade from displayed = 0
    * Fade Out  = -1
    * Fade In   = 1
    */

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Fade out element.
    /// </summary>
    public void FadeOut() {

        if ( _animator != null ) {
            _animator.SetInteger( "FadeState", -1 );
            displayed = false;
        }
    }

    /// <summary>
    /// Fade out element.
    /// </summary>
    /// <param name="speed">float - fade out speed for animation</param>
    public void FadeOut( float speed ) {
        
        if ( _animator != null ) {
            _animator.SetFloat( "FadeOutSpeed", speed );
            _animator.SetInteger( "FadeState", -1 );
            displayed = false;
        }
    }

    /// <summary>
    /// Fade in element.
    /// </summary>
    public void FadeIn() {

        if ( _animator != null ) {
            _animator.SetInteger( "FadeState", 1 );
            displayed = true;
        }
    }

    /// <summary>
    /// Fade in element.
    /// </summary>
    public void FadeIn( float speed ) {

        if ( _animator != null ) {
            _animator.SetFloat( "FadeInSpeed", speed );
            _animator.SetInteger( "FadeState", 1 );
            displayed = true;
        }
    }

    /// <summary>
    /// Apply only a half fade out
    /// from displayed element.
    /// </summary>
    public void HalfFadeOut() {

        if ( _animator != null ) {
            _animator.SetInteger( "FadeState", 0 );
        }
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _animator = GetComponent<Animator>();
    }

}
