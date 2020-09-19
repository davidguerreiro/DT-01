using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : ScriptableObject {
    public int id = 0;                              // Attack id.

    [Header("Attributes")]
    public string attackName;                       // Attack name.
    public float damage;                            // Damage done by this attack.
}
