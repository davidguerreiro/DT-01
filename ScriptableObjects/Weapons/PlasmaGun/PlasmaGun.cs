using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : ScriptableObject {
    public int id;                                  // Weapon internal id.
    
    // TODO: Implement changes by player skills.
    [Header("Name")]
    public string nameEn;                          // Weapon English name.
    public string nameEs;                          // Weapon Spanish name.
    
    [Header("Progression")]
    public int level = 1;                           // Weapon current level.
    public int currentExp = 0;                      // Weapon current exp.
    public PlasmaGunLevel[] plasmaGunLevels;        // Array with all levels data.

    [HideInInspector]
    public PlasmaGunLevel nextLevel;                // Next level data reference.

    [Header("Visuals")]
    public Sprite icon;                             // Weapon 2D icon sprite reference.

    [Header("Damage")]
    public float baseDamage;                        // Base damage caused by simple shoot impact.
    public float chargedShootBost;                  // Multiplier for charged shoots.
    public float meleeDamage;                       // Base damage caused by melee attacks.
    public float criticRate;                        // Critic damage rate. Normalized to 100.
    
    [Header("Munition")]
    public int plasma;                              // Plasma charged in the weapon.
    public int maxPlasma;                           // Maximun amount of plasma in the weapon.
    public int shootCost;                           // Plasma shoot cost - how much plasma is used to shoot base fire.
    public int chargedShootCost;                    // Plasma charged shoot cost - how much plasma is used to shoot a charged attack.

    [Header("Weapon state")]
    public bool heated;                             // If the weapon runs out of plasma, it will heated and will take more time to recharge.
    public float rechargeSpeed;                     // Weapon recharge speed - weapon charges automatically when cooling.
    public float heatedRechargeSpeed;               // Weapon recharge speed when heated. When cooling after heated, the weapon takes more time to recharge.
    public int heatedRechargeThreeshold;            // Threshold used to calculate when the recharge used heated speed or normal speed.

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
    /// Get charged boost damage
    /// value.
    /// </summary>
    /// <returns>float</returns>
    public float GetChargedDamageBaseValue() {
        return baseDamage * chargedShootBost;
    }

    /// <summary>
    /// Get experience.
    /// </summary>
    /// <param name="expGot">int - how much experience got from external action.</param>
    public void GetExp( int expGot ) {
        currentExp += expGot;

        // TODO: Add marginal exp to next level.
        if ( currentExp >= nextLevel.expRequired ) {
            IncreaseLevel();
        }
    }

    /// <summary>
    /// Check if is below heated
    /// recharhed threshold.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsBelowThreshold() {
        return plasma <= heatedRechargeThreeshold;
    }

    /// <summary>
    /// Increase plasma gun level.
    /// </summary>
    private void IncreaseLevel() {

        // increase level.
        level = nextLevel.level;
        currentExp = 0;

        // increase damage.
        baseDamage += nextLevel.baseDamage;
        criticRate += nextLevel.criticRate;

        // increase munition.
        maxPlasma += nextLevel.maxMunition;

        // increase plasma gun unique elements.
        chargedShootBost += nextLevel.chargedShootBoost;
        meleeDamage += nextLevel.meleeDamage;
        shootCost -= nextLevel.shootCost;
        chargedShootCost -= nextLevel.chargedShootCost;
        rechargeSpeed += nextLevel.rechargedSpeed;
        heatedRechargeSpeed += nextLevel.heatedRechargedSpeed;
        heatedRechargeThreeshold -= nextLevel.heatedRechargedThreeshold;

        // set up next level data object.
        nextLevel = GetLevelDataObject( level + 1 );

        // update UI.
        GamePlayUI.instance.weaponSectionUI.levelSection.UpdateUI();
    }

    /// <summary>
    /// Get level data reference.
    /// </summary>
    /// <param name="level">int - level data to be returned.</param>
    /// <returns>PlasmaGunLevel</returs>
    public PlasmaGunLevel GetLevelDataObject( int level ) {
        foreach ( PlasmaGunLevel levelData in plasmaGunLevels ) {
            if ( levelData.level == level ) {
                return levelData;
            }
        }

        return null;
    }

    /// <summary>
    /// Restart default changing values.
    /// To remove after development.
    /// </summary>
    /// <param name="toGameInit">bool - wheter to restart parameters to the default value at the beginning of the game, including those modify by skills and items</param>
    public void RestartDefaultValues( bool toGameInit = false ) {

        // restore plasma munition and heated status.
        plasma = 80;                                        // Original plasma value at the beginning of the game.
        heated = false;                                     // Original heated value at the beginning of the game.

        if ( toGameInit ) {
            maxPlasma = plasma;

            level = 1;
            currentExp = 0;

            baseDamage = 1.8f;
            chargedShootBost = 5.2f;
            meleeDamage = 3.4f;
            criticRate = 3.8f;
            maxPlasma = 80;
            shootCost = 3;
            chargedShootCost = 12;

            rechargeSpeed = 0.2f;
            heatedRechargeSpeed = 0.7f;
            heatedRechargeThreeshold = 10;

            nextLevel = GetLevelDataObject(2);
        }

    }
}
