using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance;                         // Public static class instance.
    public PlayerStats playerData;                         // Player dynamic data coming from PlayerStats scriptable.
    public PlasmaGun weapon;                               // Player's current weapon.

    [HideInInspector]
    public static string onVisionRange = "";               // This string will always contains the name of the tag currently seen by the player. Updated from Ray Shooter class in the main camera.
    
    [HideInInspector]
    public static bool onForcedCrouch = false;             // This flag will be true when the player is in an area where it has to be crouched ( like in a tunnel or something like that ).

    [Header("Inventory")]
    public Inventory basicInventory;                       // Basic items inventory data coming from Inventory scriptable.
    public Inventory craftingInventory;                    // Crafting items inventory data coming from Inventory scriptable.
    public Inventory importantInventory;                   // Important items inventory data comming from Inventory scriptable.
    private static AudioComponent _audio;                         // Audio component reference.

    [HideInInspector]
    public FPSInput playerInput;                           // Player input class.
    public GroundChecker groundChecker;                    // Player groundChecker.

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
        float damageReceived = ( damage / playerData.defense ) + UnityEngine.Random.Range( 0f, .3f );
        playerData.UpdateHitPoints( - damageReceived );
        
        // set player as invencible so it cannot get damage too fast for enemies - FPSInput class will remove invencible status automatically.
        playerInput.invencible = true;

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
            Debug.Log( "Game Over" );
        }

    }

    /// <summary>
    /// Get healed.
    /// </summary>
    /// <param name="recoveryPoints">float - how many points the player recovers.</param>
    /// <param name="playSound">bool - whether to play default recovery sounds. True by default.</param>
    /// <param name="alternativeSound">AudioClip - alternative healing sound. Not required.</param>
    public void RecoverHP( float recoveryPoints, bool playSound = true, AudioClip alternativeSound = null ) {
        
        if ( playerData.hitPoints < playerData.maxHitPoints ) {
            
            // update player damage.
            playerData.UpdateHitPoints( recoveryPoints );

            // play healing sound.
            if ( playSound ) {
                if ( alternativeSound != null ) {
                    Debug.Log(_audio);
                    _audio.PlayClip( alternativeSound );
                } else {
                    _audio.PlaySound( 3 );
                }
            }

            // display UI elements.
            if ( GamePlayUI.instance != null ) {
                GamePlayUI.instance.PlayerHealed();
            }
        }
    }

    /// <summary>
    /// Disable audio for player.
    /// </summary>
    public void DisableAudio() {
        _audio.StopAudio();
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        if ( instance == null ) {
            instance = this;
        }

        // restore playerData values after gameplay - remove from production builds.
        playerData.RestoreDefaultValues();

        // get audio component.
        _audio = GetComponent<AudioComponent>();

        // get player input class reference.
        playerInput = GetComponent<FPSInput>();

        // initialize inventories.
        basicInventory.Init();
        craftingInventory.Init();
        importantInventory.Init();
    }
    
}
