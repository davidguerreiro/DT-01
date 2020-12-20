using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLevel : ScriptableObject {
    public int level = 2;                           // To which level this data is associated with.
    public int weaponId;                            // To which weapon this level item is associated.

    [Header("Progression")]
    public int expRequired;                         // How much experience is required to reach this level.
    public GameObject[] itemsRequired;              // Items required by this level to be reached.

    [Header("Damage")]
    public float baseDamage;                        // Base damage boost when this level is reached.
    public float criticRate;                        // Critic rate boost when this level is reached.

    [Header("Munition")]
    public int maxMunition;                         // Max munition boost when this level is reached.
}
