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
    protected float maxHp;                                          // Max enemy hp.

    [Header("ParentData")]
    [SerializeField]
    protected Transform parentTransform;                            // Parent transform component reference.
    [SerializeField]
    protected Rigidbody parentRigi;                           // Parent rigibody component reference.


    [Header("Status")]
    [SerializeField]
    protected bool isAlive = true;                          // Whether the enemy is alive or has already died.
    [SerializeField]
    protected bool isMoving = false;                        // Whether the enemy is moving.
    [SerializeField]
    protected bool isRotating = false;                      // Whether the enemy is rotating.

    protected enum State {
        watching,                                           // Enemy does not move, just observe the enviroment.
        patrolling,                                         // Enemy is patrolling an area.
        combat,                                             // Enemy is enaged in combat withing the player or another target.
    };

    [SerializeField]
    protected State currentState = new State();                // Enemy state.

    [Header("Loot")]
    [SerializeField]
    protected Loot loot;                                       // Loot gameObject reference.

    [Header("Particles")]
    [SerializeField]
    protected EnemyHitParticles hitParticles;                   // Enemy hit particles reference.
    [SerializeField]
    protected EnemyDeathParticles deathParticles;               // Enemy death particles reference.

    [Header("Settings")]
    [SerializeField]
    protected GameObject parentReference;                      // Parent reference - used to remove enemy gameObject if base script is attached to children.
    [SerializeField]
    protected Renderer renderer;                               // Enemy Renderer reference.
    [SerializeField]
    protected float timeToDissapear = 5f;                      // How long till the 3D model of this enemy is removed from the scene after it gets defeated.
    [SerializeField]
    protected float dissapearAnimSpeed = 5f;                   // Dissapear animation speed.

    protected Rigidbody _rigi;                                 // Rigibody component reference.

    protected enum ColliderType {
        sphere,
        box,
        capsule,
        mesh,
    };

    [Header("Colliders")]
    [SerializeField]
    protected ColliderType[] colliderTypes;                    // Collider type. Used to check which collider we have to disable.

    protected EnemyHPBar enemyHPBar;                           // Enemy HP Bar UI component reference - used to display enemy data in the gameplay UI.
    protected Coroutine moveCoroutine;                         // Moving coroutine.
    protected Coroutine rotateCoroutine;                       // Rotating coroutine.

    /// <summary>
    /// Get damage method.
    /// </summary>
    /// <param name="externalImpactValue">float - damage value caused external attacker, usually the player.</param>
    public virtual void GetDamage( float externalImpactValue ) {
        if ( isAlive ) {
            float damageReceived = ( externalImpactValue / data.defense ) + UnityEngine.Random.Range( 0f, .5f );
            currentHp -= damageReceived;
            
            UpdateUI();

            if ( currentHp <= 0f ) {
                // remove fill straight with high speed animation.
                enemyHPBar.fill.FadeOut( 50f );
                StartCoroutine( Die() );
            } else {
                // display hit particles.
                if ( hitParticles != null ) {
                    hitParticles.DisplayHitParticles();
                }
            }
        }
    }

    /// <summary>
    /// Damage player method.
    /// </summary>
    /// <param name="Player">Player - player class reference.</param>
    public virtual void DamagePlayer( Player player ) {
        player.GetDamage( data.attack );
    }

    /// <summary>
    /// Move enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to move</param>
    /// <param name="anim">Animator - animator reference to play movement animaton.</param>
    /// <param name="animBoolVariable">string - animator bool variable name</param>
    public virtual IEnumerator Move( Vector3 destination, Animator anim = null, string animBoolVariable = "" ) {

        // TODO: Replace by rotate method.
        parentTransform.LookAt( destination );
        float remainingDistance = ( parentTransform.position - destination ).sqrMagnitude;

        if ( anim != null && animBoolVariable != "" ) {
            anim.SetBool( animBoolVariable, true );
        }

        while ( remainingDistance > float.Epsilon ) {
            isMoving = true;
            
            Vector3 newPosition = Vector3.MoveTowards( parentRigi.position, destination, data.speed * Time.deltaTime );
            parentRigi.MovePosition( newPosition );

            remainingDistance = ( parentTransform.position - destination ).sqrMagnitude;

            yield return new WaitForFixedUpdate();
        }

        isMoving = false;

        if ( anim != null && animBoolVariable != "" ) {
            anim.SetBool( animBoolVariable, false );
        }
    }

    /// <sumamry>
    /// Stop moving action.
    /// </summary>
    protected virtual void StopMoving() {
        if ( isMoving ) {
            StopCoroutine( moveCoroutine );
            moveCoroutine = null;
            isMoving = false;
        }
    }

    /// <summary>
    /// Update UI when the 
    /// enemy gets damaged by the player.
    /// </summary>
    private void UpdateUI() {
        // Update UI.
        if ( enemyHPBar.enemyID != publicID ) {
            enemyHPBar.SetUp( publicID, data.enemySprite, currentHp, maxHp );
        }

        if ( ! enemyHPBar.displayed ) {
            enemyHPBar.Display();
        }

        enemyHPBar.UpdateHP( currentHp );
    }

    /// <summary>
    /// Die method.
    /// </summary>
    public virtual IEnumerator Die() {
        isAlive = false;
        currentHp = 0f;

        RemoveCollider();

        // display death particles if required.
        if ( deathParticles ) {
            StartCoroutine( deathParticles.DisplayDeathParticles() );
        }

        // let animation happen before the loot drops.
        yield return new WaitForSeconds( .5f );
        loot.DropLoot();

        yield return null;
    }

    /// <summary>
    /// Remove collider when the
    /// enemy is defeated.
    /// </summary>
    private void RemoveCollider() {

        foreach ( ColliderType colliderType in colliderTypes ) {
            
            switch( colliderType ) {
                case ColliderType.sphere:
                    Destroy( GetComponent<SphereCollider>() );
                    break;
                case ColliderType.box:
                    Destroy( GetComponent<BoxCollider>() );
                    break;
                case ColliderType.capsule:
                    Destroy( GetComponent<CapsuleCollider>() );
                    break;
                case ColliderType.mesh:
                    Destroy( GetComponent<MeshCollider>() );
                    break;
                default:
                    break;
            }
            
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
            
        // check if player is colliding.
        if ( other.tag == "Player" ) {
            Player player = other.GetComponent<Player>();
            
            if ( ! player.playerInput.invencible ) {
                DamagePlayer( player );
            }
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay( Collider other ) {
        
        // check if player is colliding.
        if ( other.tag == "Player" ) {
            Player player = other.GetComponent<Player>();
            
            if ( ! player.playerInput.invencible ) {
                DamagePlayer( player );
            }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public virtual void Init() {
        currentHp = data.hp;
        maxHp = data.hp;

        // set UI enemy bar component.
        enemyHPBar = GameObject.Find( "EnemyHPBar" ).GetComponent<EnemyHPBar>();

        // get rigibody component reference.
        _rigi = GetComponentInParent<Rigidbody>();

    }

}
