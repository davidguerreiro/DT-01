using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ScriptableObject {
    public bool canMove = false;                        // Flag to control whether the player has control over the player character. Usually false during cutscenes and true during gameplay.
    public float hitPoints = 100f;                      // Current player hitPoints value.
    public float maxHitPoints = 100f;                   // Current player max hitPoints value.
    public float currentEnergy = 60f;                   // Current player energy. Energy is consumed by physical actions like running and jumping.
    public float maxEnergy = 60f;                       // Current player maximun energy.
    public int defense = 2;                             // Player defense values. The higher this value, the less the damage player will get.
    public float physicalCondition = 2f;                // The physical condition defines how fast the player recovers energy.
    public int shards = 0;                              // Current player cristal shards. Shards are used to unlock new abilities.
    public int maxShards = 9999;                        // Maximun number of shards the player can hold without spending.
}
