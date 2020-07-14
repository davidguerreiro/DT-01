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
    public float shootCost;                         // Plasma shoot cost - how much plasma is used to shoot base fire.
    public float chargedShootCost;                  // Plasma charged shoot cost - how much plasma is used to shoot a charged attack.

    [Header("Weapon state")]
    public bool heated;                             // If the weapon runs out of plasma, it will heated and will take more time to recharge.
    public float rechargeSpeed;                     // Weapon recharge speed - weapon charges automatically when cooling.
    public float headerRechargeSpeed;               // Weapon recharge speed when heated. When cooling after heated, the weapon takes more time to recharge.

    /// <summary>
    /// Consume weapon's plasma
    /// when the weapon is shooter.
    /// </summary>
    /// <param name="isCharged">bool - if charged shoot, consumes more plasma when shooting.</param>
    public void UpdatePlasma( bool isCharged = false ) {
        float toSubstract = ( isCharged ) ? chargedShootCost : shootCost;

        plasma -= (int) toSubstract;

        // check if heated.
        if ( plasma <= 0 ) {
            heated = true;
        }
    }
}
