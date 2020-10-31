using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager instance;                         // Menu static instance reference.
    public bool displayed;                                      // Flag to control display status.

    [Header("Sections")]
    public FadeElement background;                              // Menu background fade class reference.
    public MenuNavigation menuNavigation;                       // Menu navigation class reference.
    public MenuContent menuContent;                             // Menu content class reference.

    private Coroutine _animRoutine;                             // Display / hide coroutine.
    private AudioComponent _audio;                              // Audio component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Display menu when the game is paused.
    /// </summary>
    public void Display() {
        if ( _animRoutine == null ) {
            _animRoutine = StartCoroutine( DisplayRoutine() );
        }
    }

    /// <summary>
    /// Hide menu when the pause is cancelled by the user.
    /// </summary>
    public void Hide() {
        if ( _animRoutine == null ) {
            _animRoutine = StartCoroutine( HideRoutine() );
        }
    }

    /// <sumamry>
    /// Display coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator DisplayRoutine() {
        _audio.PlaySound( 0 );

        // display menu background and sections.
        background.FadeIn();
        yield return new WaitForSeconds( 1f );
        menuNavigation.gameObject.SetActive( true );
        menuContent.gameObject.SetActive( true );

        displayed = true;
        _animRoutine = null;        
    }

    /// <sumamry>
    /// Hide coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator HideRoutine() {
        _audio.PlaySound( 1 );

        // display menu background and sections.
        menuContent.gameObject.SetActive( false );
        menuNavigation.gameObject.SetActive( false );
        background.FadeOut();
        yield return new WaitForSeconds( 1f );

        displayed = false;
        _animRoutine = null;        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
