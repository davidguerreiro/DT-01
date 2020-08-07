using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance;                          // Public static class instance.
    public PlayerStats playerData;                          // Player dynamic data coming from PlayerStats scriptable.

    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    void Start() {
        Init();
    }

    /// <summary>
    /// Check if the player is in
    /// control of the game.
    /// </summary>
    /// <returns>bool</returns>
    public bool CanMove() {
        return playerData.canMove;
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // restore playerData values after gameplay - remove from production builds.
        playerData.RestoreDefaultValues();
    }
    
}
