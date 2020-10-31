using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeElement : MonoBehaviour {
    public bool displayed;                                  // Flag to control displayed status.

    public enum Type {
        image,
        text,
    };
    
    public Type type;                                       // To which type of element this script is attached. Text and image requires different components for manipulatin the colour property raw.
    private Animator _animator;                             // Animator component reference.
    private Image _image;                                   // Image component reference.
    private Text _text;                                     // Text component reference.

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
    /// <param name="speed">float - fade in speed for animation</param>
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
            _animator.SetInteger( "FadeState", 2 );
            displayed = false;
        }
    }

    /// <summary>
    /// Apply only a half fade out
    /// from displayed element.
    /// </summary>
    /// <param name="speed">float- half fade Out speed for animation</param>
    public void HalfFadeOut( float speed ) {

        if ( _animator != null ) {
            _animator.SetFloat( "FadeInSpeed", speed );
            _animator.SetInteger( "FadeState", 2 );
            displayed = false;
        }
    }

    /// <summary>
    /// Get raw colour component.
    /// </summary>
    private void GetRawColourComponent() {

        switch ( type ) {
            case Type.image:
                _image = GetComponent<Image>();
                break;
            case Type.text:
                _text = GetComponent<Text>();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Display element without fade
    /// animation.
    /// </summary>
    public void RawDisplay() {
        
        // image.
        if ( type == Type.image && _image != null ) {
            _image.color = new Color( _image.color.r, _image.color.g, _image.color.b, 1f );
            displayed = true;
        }

        // text.
        if ( type == Type.text && _text != null ) {
            _text.color = new Color( _text.color.r, _text.color.g, _text.color.b, 1f );
            displayed = true;
        }
    }

    /// <summary>
    /// Hide element without fade
    /// animation.
    /// </summary>
    public void RawHide() {

        // image.
        if ( type == Type.image && _image != null ) {
            _image.color = new Color( _image.color.r, _image.color.g, _image.color.b, 0f );
            displayed = false;
        }

        // text.
        if ( type == Type.text && _text != null ) {
            _text.color = new Color( _text.color.r, _text.color.g, _text.color.b, 0f );
            displayed = false;
        }
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _animator = GetComponent<Animator>();

        // get raw colour component reference.
        GetRawColourComponent();
    }

}
