using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance;                           // Public static class instance - this class must be instantiated just once.
    public bool cursorEnabled;                                  // Wheter the cursor is enabled or disabled in the game.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }

        if ( cursorEnabled ) {
            DisplayCursor();
        } else {
            HideCursor();
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        // CheckCursor();
    }

    /// <summary>
    /// Update cursor based on public
    /// attribute. This attribute is modified
    /// by game logic through the game.
    /// </summary>
    private void CheckCursor() {

        if ( cursorEnabled ) {
            DisplayCursor();
        } else {
            HideCursor();
        }
    }

    /// <summary>
    /// Display cursor.
    /// </summary>
    public void DisplayCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Hide cursor.
    /// </summary>
    public void HideCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
