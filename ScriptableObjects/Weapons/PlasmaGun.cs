using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : ScriptableObject {

    // TODO: Implement changes by player skills.

    [Header("Damage")]
    public float baseDamage;                        // Base damage caused by simple shoot impact.
    public float chargedShootBost;                  // Multiplier for charged shoots.
    public float meleeDamage;                       // Base damage caused by melee attacks.
    
    [Header("Munition")]
    public int plasma;                              // Plasma charged in the weapon.
    public int maxPlasma;                           // Maximun amount of plasma in the weapon.
    public int shootCost;                         // Plasma shoot cost - how much plasma is used to shoot base fire.
    public int chargedShootCost;                  // Plasma charged shoot cost - how much plasma is used to shoot a charged attack.

    [Header("Weapon state")]
    public bool heated;                             // If the weapon runs out of plasma, it will heated and will take more time to recharge.
    public float rechargeSpeed;                     // Weapon recharge speed - weapon charges automatically when cooling.
    public float heatedRechargeSpeed;               // Weapon recharge speed when heated. When cooling after heated, the weapon takes more time to recharge.
    public int heatedRechargeThreeshold;            // Threshold used to calculate when the recharge used heated speed or normal speed.


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Consume weapon's plasma
    /// when the weapon is shooter.
    /// </summary>
    /// <param name="isCharged">bool - if charged shoot, consumes more plasma when shooting.</param>
    public void UpdatePlasma( bool isCharged = false ) {
        int toSubstract = ( isCharged ) ? chargedShootCost : shootCost;

        plasma -= toSubstract;

        if ( plasma < 0 ) {
            plasma = 0;
        }

        // check if heated.
        if ( plasma <= 0 ) {
            heated = true;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // restore values to original start level values.
        plasma = maxPlasma;
        heated = false;
    }

    /// <summary>
    /// Restart default changing values.
    /// </summary>
    /// <param name="toGameInit">bool - wheter to restart parameters to the default value at the beginning of the game, including those modify by skills and items</param>
    public void RestartDefaultValues( bool toGameInit = false ) {

        // restore plasma munition and heated status.
        plasma = 80;                                        // Original plasma value at the beginning of the game.
        heated = false;                                     // Original heated value at the beginning of the game.

        if ( toGameInit ) {
            maxPlasma = plasma;                             
        }

    }
}
