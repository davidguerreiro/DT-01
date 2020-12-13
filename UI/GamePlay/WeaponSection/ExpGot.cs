using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGot : MonoBehaviour {

    public bool displayed;                                  // Whether this element is displayed in game screen.

    [Header("Settings")]
    public float secondsDisplayed;                          // For how long it is displayed on the screen.

    private TextComponent _text;                            // Text component reference.
    private Animator _anim;                                 // Animator component reference.
    private float displayedCounter;                         // Displayed counter to check when to hide this text.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        if ( ! GameManager.instance.isPaused && displayed ) {
            CheckIfToHide();
        }
    }

    /// <summary>
    /// Display element.
    /// </summary>
    /// <param name="expToDisplay">int - how much experience to display in the screen.</param>
    public void Show( int expToDisplay ) {
        _text.UpdateContent( "+ " + expToDisplay.ToString() + " Exp" );

        if ( ! displayed ) {
            _anim.SetBool( "Displayed", true );
            displayed = true; 
        } else {
            _anim.SetTrigger( "Flash" );
        }

        displayedCounter = 0;
    }

    /// <summary>
    /// Hide element.
    /// </summary>
    public void Hide() {
        _anim.SetBool( "Displayed", false );
        displayed = false;
        displayedCounter = 0f;
    }

    /// <summary>
    /// Check if element needs to be
    /// hidden.
    /// </summary>
    private void CheckIfToHide() {
        // assumes 1sec = 60 frames per sec.
        if ( displayedCounter >= ( secondsDisplayed * 60f ) ) {
            Hide();
        } else {
            displayedCounter++;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get text component reference.
        _text = GetComponent<TextComponent>();

        // get animator component.
        _anim = GetComponent<Animator>();
    }
}
