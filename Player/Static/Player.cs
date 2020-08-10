using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance;                          // Public static class instance.
    public PlayerStats playerData;                          // Player dynamic data coming from PlayerStats scriptable.
    private AudioComponent _audio;                          // Audio component reference.

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
    /// Get damage.
    /// </summary>
    /// <param name="damage">float - how much damage the player has received</param>
    public void GetDamage( float damage ) {

        // update player damage.
        playerData.UpdateHitPoints( damage );

        if ( playerData.hitPoints > 0f ) {

            // generate a radom number between all the damage sounds keys for player ( currently 2 ).
            int index = Random.Range( 1, 3 );

            // play damage audio.
            _audio.PlaySound( index );

            // display UI damage elements.
            if ( GamePlayUI.instance != null ) {
                GamePlayUI.instance.PlayerDamaged();
            }

        } else {
            // player has no more hit points - gameover
            // TODO: Add game over call here.
        }

    }

    /// <summary>
    /// Get healed
    /// </summary>
    /// <param name="recoveryPoints">float - how many points the player recovers</param>
    public void RecoverHP( float recoveryPoints ) {
        
        // update player damage.
        playerData.UpdateHitPoints( recoveryPoints );

        if ( playerData.hitPoints < playerData.maxHitPoints ) {

            // play healing sound.
            _audio.PlaySound( 3 );

            // display UI elements.
            if ( GamePlayUI.instance != null ) {
                GamePlayUI.instance.PlayerHealed();
            }
        }
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // restore playerData values after gameplay - remove from production builds.
        playerData.RestoreDefaultValues();

        // get audio component.
        _audio = GetComponent<AudioComponent>();
    }
    
}
