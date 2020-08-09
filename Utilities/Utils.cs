using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour {
    public static Utils instance;                   // Class public static instance - this is the unique instance of this class.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    // Update is called once per frame.
    void Update() {
        
        // Quit game when pressing escape key.
        if ( Input.GetKeyDown( "escape" ) ) {
            QuitGame();
        }

        // Open debug menu.
        if ( Input.GetKeyDown( "p" ) ) {
            LoadDebugMenu();
        }
    }

    /// <summary>
    /// Load debug menu.
    /// </summary>
    private void LoadDebugMenu() {
        SceneManager.LoadScene( "DebugMenu" );
    }

    /// <summary>
    /// Load scene.
    /// </summary>
    /// <param name="sceneName">string - scene name to be loaded.</param>
    public void LoadScene( string sceneName ) {
        SceneManager.LoadScene( sceneName );
    }

    /// <summary>
    /// Quit game.
    /// </summary>
    private void QuitGame() {
        Application.Quit();
    }
}
