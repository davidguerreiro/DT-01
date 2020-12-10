using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSection : MonoBehaviour {

    [Header("Settings")]
    public Vector3 damageInstanceInitPosition;                      // Damage text instance init position.
    public Color criticalColor;                                     // Damage text color used when an impact is critic.

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
        //string damageToDisplay = enemyDamage.ToString("F2").Replace(",", "." );
        // damageText.UpdateContent( damageToDisplay );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get object pool reference to instantiate damage text elements.
        _pool = GetComponent<ObjectPool>();
    }
}
