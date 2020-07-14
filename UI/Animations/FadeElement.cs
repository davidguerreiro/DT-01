using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeElement : MonoBehaviour {
    public bool displayed;                                  // Flag to control displayed status.
    private Animator _animator;                             // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Fade out element.
    /// </summary>
    public void FadeOut() {
        
        if ( _animator != null ) {
            _animator.SetTrigger( "FadeOut" );
            displayed = false;
        }
    }

    /// <summary>
    /// Fade out element.
    /// </summary>
    /// <param name="speed">float - fade out speed for animation</param>
    public void FadeOut( float speed ) {

        Debug.Log( _animator );
        
        if ( _animator != null ) {
            _animator.SetFloat( "FadeOutSpeed", speed );
            _animator.SetTrigger( "FadeOut" );
            displayed = false;
        }
    }

    /// <summary>
    /// Fade in element.
    /// </summary>
    public void FadeIn() {

        if ( _animator != null ) {
            _animator.SetTrigger( "FadeIn" );
            displayed = true;
        }
    }

    /// <summary>
    /// Fade in element.
    /// </summary>
    public void FadeIn( float speed ) {

        if ( _animator != null ) {
            _animator.SetFloat( "FadeInSpeed", speed );
            _animator.SetTrigger( "FadeIn" );
            displayed = true;
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
