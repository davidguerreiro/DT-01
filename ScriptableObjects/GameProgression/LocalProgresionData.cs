using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalProgresionData : ScriptableObject {
    public int id;                                              // Local progression id.
    public string sceneName;                                    // Local progression scene name.

    public struct EnemyGroups {
        EnemyGroup enemyGroup;                                  // Enemy group instance reference..
        bool defeated;                                          // Flag to control if this enemy group has been defeated.
    }

    public EnemyGroups[] enemyGroups;                            // Array of enemy groups.

}
