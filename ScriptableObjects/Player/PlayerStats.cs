using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ScriptableObject {
    [Header("Player's movement")]
    public bool canMove = false;                        // Flag to control whether the player has control over the player character. Usually false during cutscenes and true during gameplay.

    [Header("Player's health")]
    public float hitPoints = 100f;                      // Current player hitPoints value.
    public float maxHitPoints = 100f;                   // Current player max hitPoints value.

    [Header("Player's physical condition")]
    public float energy = 60f;                   // Current player energy. Energy is consumed by physical actions like running and jumping.
    public float maxEnergy = 60f;                       // Current player maximun energy.

    [Header("Player's stats")]
    public int defense = 2;                             // Player defense values. The higher this value, the less the damage player will get.
    public float physicalCondition = 2f;                // The physical condition defines how fast the player recovers energy.

    [Header("Shards")]
    public int shards = 0;                              // Current player cristal shards. Shards are used to unlock new abilities.
    public int maxShards = 9999;                        // Maximun number of shards the player can hold without spending.

    /// <summary>
    /// Update player hit points.
    /// </summary>
    /// <param name="value">float - value used to update player hit points.</param>
    /// <returns>void</returns>
    public void UpdateHitPoints( float value ) {
        
        hitPoints += value;

        if ( hitPoints < 0f ) {
            hitPoints = 0f;
        }

        if ( hitPoints > maxHitPoints ) {
            hitPoints = maxHitPoints;
        }
    }

    /// <summary>
    /// Update maxHitPoints. Sets
    /// a new value for player's max
    /// hit points.
    /// </summary>
    /// <param name="newValue">float - new value for maximun hit points for the player</param>
    /// <returns>void</returns>
    public void SetNewMaxHitPoints( float newValue ) {

        // update current hit points to keep the player's health relative to
        // current health.
        if ( hitPoints == maxHitPoints ) {
            hitPoints = newValue;
        } else {
            hitPoints += ( maxHitPoints + newValue ) - maxHitPoints;
        }

        maxHitPoints = newValue;

        // ensure current hit points are not higher than max points after
        // the update.
        if ( hitPoints > maxHitPoints ) {
            hitPoints = maxHitPoints;
        }
    }

    /// <summary>
    /// Update current energy.
    /// Energy is used when the player
    /// runs or jumps.
    /// </summary>
    /// <param name="value">float - value to add or substract in the energy bar</parma>
    /// <retunrs>void</returns>
    public void UpdateEnergy( float value ) {
        
        energy += value;

        if ( energy > maxEnergy ) {
            energy = maxEnergy;
        }

        if ( energy < 0f ) {
            energy = 0f;
        }
    }

    /// <summary>
    /// Update max energy parameter.
    /// </summary>
    /// <param name="newValue">float - new max energy value</param>
    /// <returns>void</returns>
    public void SetMaxEnergy( float newValue ) {
        maxEnergy = newValue;

        if ( energy > maxEnergy ) {
            energy = maxEnergy;
        }
    }
    
    /// <summary>
    /// Set new defense value.
    /// </summary>
    /// <param name="newValue">int - new defense value.</param>
    /// <returns>void</returns>
    public void SetDefenseValue( int newValue ) {
        defense = newValue;
    }

    /// <summary>
    /// Set new physical condition
    /// value.
    /// </summary>
    /// <parma name="newValue">float - new physical condition value</param>
    /// <returns>void</returns>
    public void SetPhysicalConditionValue( float newValue ) {
        physicalCondition = newValue;
    }

    /// <summary>
    /// Update current player's shards.
    /// </summary>
    /// <parma name="value">int - shards to add or substract to the player's bad</param>
    /// <returns>void</returns>
    public void UpdateShards( int value ) {
        shards += value;

        if ( shards > maxShards ) {
            shards = maxShards;
        }

        if ( shards < 0 ) {
            shards = 0;
        }
    }

    /// <summary>
    /// Set values as they should when
    /// initialising the level or after
    /// respawn in a checkpoin.
    /// </summary>
    /// <returns>void</returns>
    public void RecoverAllStats() {
        hitPoints = maxHitPoints;
        energy = maxEnergy;
    }

    /// <summary>
    /// Restore changing values to
    /// defautls.
    /// This is a testing method and must not
    /// be used in produciton builds.
    /// </summary>
    /// <param name="toGameInit">bool - wheter to restart parameters to the default value at the beginning of the game, including those modify by skills and items</param>
    /// <returns>void</returns>
    public void RestoreDefaultValues( bool toGameInit = false ) {

        canMove = true;
        hitPoints = maxHitPoints;
        energy = maxEnergy;
        shards = 0;

        if ( toGameInit ) {
            defense = 2;
            physicalCondition = 2f;
        }
    }
}
