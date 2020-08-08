using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour {
    public static GamePlayUI instance;                      // Class static instance reference - unique class instance.
    public PlayerDamagedComponent playerDamagedComponent;   // Player damage component wrapper for blood damage elemnts in the gameplay UI.

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
}
