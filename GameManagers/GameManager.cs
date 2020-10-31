using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;                         // Public static instance of this variable.
    [Header("Status")]
    public bool inGamePlay = true;                              // Flag to control whether the player can have gameplay control.
    public bool isPaused = false;                               // Flag to control paused game.

    [Header("Private Settings")]
    public bool canBePaused = true;                             // Whether the game can be paused by player.


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update() {
        if ( canBePaused ) {
            CheckForUserInput();
        }
    }

    /// <summary>
    /// Check for user input events.
    /// </summary>
    private void CheckForUserInput() {
        
        if ( Input.GetKeyDown( "escape" ) || Input.GetKeyDown( "return" ) ) {
            if ( ! isPaused && inGamePlay && ! MenuManager.instance.displayed ) {
                PauseGame();
            } else if ( isPaused && inGamePlay && MenuManager.instance.displayed ) {
                ResumeGame();
            }
            
        }

    }

    /// <summary>
    /// Pause game.
    /// </summary>
    public void PauseGame() {
        isPaused = true;
        Time.timeScale = 0;

        // display menu.
        MenuManager.instance.Display();

        UIManager.instance.cursorEnabled = true;
    }

    /// <summary>
    /// Resume game.
    /// </summary>
    public void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1;

        // hide menu.
        MenuManager.instance.Hide();
        UIManager.instance.cursorEnabled = false;
    }
}
