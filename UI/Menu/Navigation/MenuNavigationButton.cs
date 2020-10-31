using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigationButton : MonoBehaviour {
    public int id;                                          // Section id.
    public string sectionName;                              // Section name - this is to identify which section will be loaded by this button.
    public bool active = false;                             // Flag to control if this is the active section.
    public FadeElement textAnim;                            // Fade text component reference.
    public MenuContent contentSection;                      // Menu content section manager class reference.

    [HideInInspector]
    private AudioComponent _audio;                          // Audio component reference.
    private Button _button;                                 // Button component reference.
    private MenuNavigation _menuNavigation;                 // Menu navigation class component reference.

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
            
            if ( _menuNavigation != null ) {
                _menuNavigation.DisableAll( id );
            }
            
            // load this section into the content section.
            contentSection.SwitchSection( id );
        }
    }

    /// <summary>
    /// Unselected logic.
    /// Usually called when another section
    /// has been selected.
    /// </summary>
    public void UnSelected() {
        if ( active ) {
            active = false;
            Unhover();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();

        // get button component reference.
        _button = GetComponent<Button>();

        // get menu navigation component reference.
        _menuNavigation = GetComponentInParent<MenuNavigation>();
    }
}
