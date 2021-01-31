using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {
    public string optionLogic;                                  // Scene to load when clicking this button.
    public FadeElement textAnim;                                // Fade text component reference.
    private bool loaded = false;                                // Flag to avoid multiclick errors.
    private AudioComponent _audio;                              // Audio component reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Hover by mouse logic.
    /// </summary>
    public void Hover() {
        if ( ! textAnim.displayed ) {
            _audio.PlaySound();
            textAnim.FadeIn( 1f );      // Standard fade speed.
        }
    }

    /// <summary>
    /// Unhover by mouse logic.
    /// </summary>
    public void Unhover() {
        if ( textAnim.displayed ) {
            textAnim.HalfFadeOut();
        }
    }

    /// <summary>
    /// Selected logic.
    /// </summary>
    public void Selected() {
        if ( ! loaded ) {
            StartCoroutine( RunOptionLogic() );
        }
    }

    /// <summary>
    /// Click actio coroutine
    /// to load the next screen,
    /// scene or close the game
    /// based on option selected.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator RunOptionLogic() {
        loaded = true;

        switch ( optionLogic ) {
            case "Play":
                // play second sound in the sounds array ( position 1 ).
                _audio.PlaySound( 1 );
                StartCoroutine( MainMenuManager.instance.StartGameAnim() );

                // wait two extra seconds after the animation finishes before loading next scene.
                yield return new WaitForSecondsRealtime( MainMenuManager.instance.startGameAnimationWait + 4f );
                SceneManager.LoadScene( "CrystalCaves" );
                // SceneManager.LoadScene( "BattleTesting" );
                break;
            case "Options":
                // TODO: Play any animation or sound required.
                // TODO: Open panel settings.
                break;
            case "Credits":
                // TODO: Play any animation or sound required.
                // TODO: Load credits scene.
                break;
            case "Quit":
                Application.Quit();
                break;
            default:
                yield break;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
