using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : ScriptableObject {
    public int id = 0;                      // Enemy ID.

    [Header("UI")]
    public string enemyName;                // Enemy display name.                     
    public Sprite enemySprite;               // Enemy 2D sprite. Displayed in the UI during combat and in the Data Center.
    public int dataNumber;                  // Data number for data center funcionality.

    [Header("Base Stats")]
    public float hp;                        // Maximun hp for this enemy.
    public float attack;                    // Used to calculate damage caused to the player.
    public float defense;                   // Used to calculate damage done by the player to this enemy.
    public float speed;                     // Used to calculate movement speed.
    public float attackRatio;               // Used to calculate enemy attack ratio.
    
    [Header("Description")]
    [TextArea]
    public string descriptionEsp;           // Enemy Spanish description, displayed in the Data Center module.
    [TextArea]
    public string descriptionEng;           // Enemy English description, displayed in the Data Center module.

    [Serializable]
    public struct Actions {
        public EnemyAttack action;          // Attack which can be performed by the enemy.
        public int rate;                    // Action rate - used to calculate the chances that this enemy will perform this attack.
    };

    [Header("Actions")]
    public Actions[] attacks = new Actions[1];  // Action attacks which can be performed by this enemy.
    

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
