using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        
        // quit game when pressing escape key.
        if ( Input.GetKeyDown( "escape" ) ) {
            QuitGame();
        }
    }

    /// <summary>
    /// Quite game.
    /// </summary>
    private void QuitGame() {
        Application.Quit();
    }
}
