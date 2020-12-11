using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : ScriptableObject {
    public int id = 0;                      // Enemy ID.

    [Header("UI")]
    public string enemyName;                // Enemy display name.                     
    public Sprite enemySprite;              // Enemy 2D sprite. Displayed in the UI during combat and in the Data Center.
    public int dataNumber;                  // Data number for data center funcionality.

    [Header("Base Stats")]
    public float hp;                        // Maximun hp for this enemy.
    public float attack;                    // Used to calculate damage caused to the player.
    public float defense;                   // Used to calculate damage done by the player to this enemy.
    public float speed;                     // Used to calculate movement speed.
    public float luck;                      // Used to calculate defense to critic damage.
    public float meleeVulnerable;           // Used to calculate how much damage can this enemy get from melee attacks, if any.

    [Header("Action Rations")]
    public float attackRatio;               // Used to calculate enemy attack ratio.
    public float randomMovementRatio;       // Used to calculate how often the enemy moves around randomly in watch mode.

    [Header("Movement")]
    public float randXMovementAmplitude;               // Amplitude for random movement in the X axis.
    public float randZMovementAmplitude;               // Amplitude for random movement in the Y axis.

    [Serializable]
    public struct Actions {
        public EnemyAttack attack;          // Attack which can be performed by the enemy.
        public int rate;                    // Action rate - used to calculate the chances that this enemy will perform this attack.
    };

    [Header("Actions")]
    public Actions[] attacks = new Actions[1];  // Action attacks which can be performed by this enemy.

    [Header("Type")]
    public EnemyType enemyType;                 // Enemy type reference.
    public bool isBoss;                         // Whether this enemy is a boss or not.
    public int expGiven;                        // Experience given when this enemy is defeated.

    [Header("Description")]
    [TextArea]
    public string descriptionEsp;           // Enemy Spanish description, displayed in the Data Center module.
    [TextArea]
    public string descriptionEng;           // Enemy English description, displayed in the Data Center module.

    [Header("Strategy")]
    [TextArea]
    public string stratagyEsp;              // Enemy strategy in Spanish.
    [TextArea]
    public string strategyEng;              // Enemy strategy in English.

    [Header("Extra Data")]
    public int defeated = 0;                // Enemies of this tipe defeated by the player across the entire game.
    
    [Header("Debug")]
    public bool reset;                      // Defines when to reset this data each time we play the game. To be set as false for real gameplay.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        if ( reset ) {
            ResetData();
        }
    }

    /// <summary>
    /// Reset enemy data.
    /// </summary>
    public void ResetData() {
        defeated = 0;
    }


}
