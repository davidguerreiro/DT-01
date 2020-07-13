using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance;                          // Public static class instance.
    public PlayerStats playerData;                          // Player dynamic data coming from PlayerStats scriptable.

    /// <summary>
    /// Check if the player is in
    /// control of the game.
    /// </summary>
    public bool CanMove() {
        return playerData.canMove;
    }
}
