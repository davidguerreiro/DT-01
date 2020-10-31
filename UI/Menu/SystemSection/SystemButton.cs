using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemButton : MonoBehaviour {
    public int id;                                          // Button id.
    [TextArea]
    public string description;                              // Button action description.

    private AudioComponent _audio;                          // Audio component.
    private Button _button;                                 // Button component.
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Hover.
    /// </summary>
    public void Hover() {
        if ( _button.interactable ) {
            _audio.PlaySound(0);
        }
    }
    
    /// <summary>
    /// Selected.
    /// </summary>
    public void Selected() {
        switch( id ) {
            case 0: // Save game.
                break;
            case 1: // Load game.
                break;
            case 2: // Settings.
                break;
            case 3: // Quit application.
                // TODO: Call description section to display description in the UI.
                Application.Quit();
                break;
            default:
                break;
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
    }
}
