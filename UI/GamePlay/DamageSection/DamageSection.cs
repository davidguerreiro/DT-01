using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSection : MonoBehaviour {

    private ObjectPool _pool;                                       // Damage text object pool.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Update damage value.
    /// </summary>
    /// <parma name="enemyDamage">float - enemy damage got to display</param>
    /// <param name="isCritic">bool - wheter the damage got is critic.False by default.</param>
    public void DisplayDamage( float enemyDamage, bool isCritic = false ) {
        GameObject damageTextPrefab = _pool.SpawnPrefab();

        if ( damageTextPrefab != null ) {
            damageTextPrefab.GetComponent<DamageText>().Display( enemyDamage, isCritic );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get object pool reference to instantiate damage text elements.
        _pool = GetComponent<ObjectPool>();
    }
}
