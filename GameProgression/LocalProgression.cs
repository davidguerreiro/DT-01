using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalProgression : MonoBehaviour {
    public static LocalProgression instance;                    // Public static class instance.

    [Header("Local Progression Data Source")]
    public EnemyGroupsProgression enemyGroups;                  // Enemy groups progression data reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    } 
}
