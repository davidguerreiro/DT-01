using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour {
    public static GamePlayUI instance;                      // Class static instance reference - unique class instance.
    public PlayerDamagedComponent playerDamagedComponent;   // Player damage component wrapper for blood damage elemnts in the gameplay UI.
    public PlayerHealComponent playerHealComponent;         // Player health component wrapper to be used when the player recovers health.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Display player damaged
    /// animation in the UI.
    /// </summary>
    public void PlayerDamaged() {

        if ( playerDamagedComponent.bloodPanel != null && playerDamagedComponent.bloodImage != null ) {
            playerDamagedComponent.bloodPanel.Display();
            playerDamagedComponent.bloodImage.Display();
        }
    }

    /// <summary>
    /// Display player healing
    /// animation in the UI.
    /// </summary>
    public void PlayerHealth() {

        if ( playerHealComponent.healthPanel != null ) {
            playerHealComponent.healthPanel.Display();
        }
    }
}
