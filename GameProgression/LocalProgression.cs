using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalProgression : MonoBehaviour {
    public static LocalProgression instance;                    // Public static class instance.

    [Header("Local Progression Data Source")]
    public EnemyGroupsProgression enemyGroups;                  // Enemy groups progression data reference.
}
