using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigationButton : MonoBehaviour {
    // TODO: Add section class reference.
    public string sectionName;                              // Section name - this is to identify which section will be loaded by this button
    public bool active = false;                             // Flag to control if this is the active section.
    public FadeElement textAnim;                            // Fade text component reference.

    [HideInInspector]
    public Coroutine loadingSection;                       // Loading section coroutine reference.
    private AudioComponent _audio;                          // Audio component reference.
    private Button _button;                                 // Button component reference.

    // Start is called before the first frame update
    void Start() {
        Init();   
    }

    /// <summary>
    /// Hover by mouse logic.
    /// </summary>
    public void Hover() {
        if ( ! active && _button.interactable && ! textAnim.displayed ) {
            _audio.PlaySound( 0 );          // Play first sound in the sound pool.
            textAnim.FadeIn( 1f );          // Standard fade speed;
        }
    }

    /// <summary>
    /// Unhover by mouse logic.
    /// </summary>
    public void Unhover() {
        if ( ! active && _button.interactable && textAnim.displayed ) {
            textAnim.HalfFadeOut();
        }
    }

    /// <summary>
    /// Selected logic.
    /// </summary>
    public void Selected() {
        if ( ! active ) {
            active = true;
            loadingSection = StartCoroutine( LoadSection() );
        }
    }

    /// <summary>
    /// Load section attached to
    /// this button in the game menu.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator LoadSection() {
        // TODO: Trigger load section attached to this button here.
        loadingSection = null;
        yield break;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();

        // get button component reference.
        _button = GetComponent<Button>();
    }
}
