using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance;                           // Public static class instance - this class must be instantiated just once.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
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
