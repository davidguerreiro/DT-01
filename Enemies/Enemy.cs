using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField]
    protected int publicID;                                         // Public id, this is unique for each enemy instance created, only by scene. 

    [Header("Data Source")]
    [SerializeField]
    protected EnemyData data;                                       // Enemy data scriptable object. Contains all this enemy default values.
    protected float currentHp;                                      // Current enemy HP.
    protected float maxHP;                                          // Max enemy hp.


    [Header("Status")]
    [SerializeField]
    protected bool isAlive = true;                             // Whether the enemy is alive or has already died.

    protected enum State {
        watching,                                           // Enemy does not move, just observe the enviroment.
        patrolling,                                         // Enemy is patrolling an area.
        combat,                                             // Enemy is enaged in combat withing the player or another target.
    };

    [SerializeField]
    protected State currentState = new State();                // Enemy state.

    // TODO: Add reference to UI component.

    /// <summary>
    /// Get damage method.
    /// </summary>
    /// <param name="externalImpactValue">float - damage value caused external attacker, usually the player.</param>
    public virtual void GetDamage( float externalImpactValue ) {
        float damageReceived = ( externalImpactValue / data.defense ) + UnityEngine.Random.Range( 0f, 2f );
        Debug.Log( damageReceived );                   
    }
    
    /// <summary>
    /// Init class method.
    /// </summary>
    public virtual void Init() {
        currentHp = data.hp;
        maxHP = data.hp;
    }
}
