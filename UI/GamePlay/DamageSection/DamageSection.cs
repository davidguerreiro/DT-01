using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSection : MonoBehaviour {

    [Header("Components")] 
    public TextComponent damageText;                                // Damage text component.
    // Start is called before the first frame update
    void Start() {
        
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
        damageText.UpdateContent( enemyDamage.ToString("F2") );
    }
}
