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
    protected float attack;                                         // Base enemy attack value. Used to calculate damage caused to player. Enemy attacks might increase or decrease this value.

    [Header("Status")]
    [SerializeField]
    protected bool isAlive = true;                          // Whether the enemy is alive or has already died.
    [SerializeField]
    protected bool isMoving = false;                        // Whether the enemy is moving.
    [SerializeField]
    protected bool isRotating = false;                      // Whether the enemy is rotating.
    [SerializeField]
    protected bool isAttacking = false;                     // Whether the enemy is performing an attack.
    [SerializeField]
    protected bool inBattle = false;                        // Whether the enemy is engaged in battle.
    [SerializeField]
    protected bool isLookingAtPlayer = false;               // Whether the enemy is looking at the player continuosly.
    public enum State {
        none,                                               // This state is the default value and has no impact over enemy behavioir or logic.
        watching,                                           // Enemy does not move, just observe the enviroment.
        patrolling,                                         // Enemy is patrolling an area.
        battling,                                             // Enemy is enaged in combat withing the player or another target.
        returning,                                          // Enemy has leave their enemy group area and it is returning to their initial position.
    };

    [SerializeField]
    protected State currentState = new State();                // Enemy state.
    protected State initialState = new State();                // Initial enemy state, used for logic reference.

    [Header("Loot")]
    [SerializeField]
    protected Loot loot;                                       // Loot gameObject reference.

    [Header("Particles")]
    [SerializeField]
    protected EnemyHitParticles hitParticles;                   // Enemy hit particles reference.
    [SerializeField]
    protected EnemyDeathParticles deathParticles;               // Enemy death particles reference.
    
    [Header("Enemy Navigators")]
    [SerializeField]
    protected EnemyNavigator[] navigators;                      // Enemy navigators - used to detect other entities in the game scene ( player, walls, other enemies, etc ).

    [Header("Settings")]
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

    [HideInInspector]
    public EnemyGroup enemyGroup;                           // Current enemy's enemy group.

    protected EnemyHPBar enemyHPBar;                           // Enemy HP Bar UI component reference - used to display enemy data in the gameplay UI.
    protected Coroutine moveCoroutine;                         // Moving coroutine.
    protected Coroutine rotateCoroutine;                       // Rotating coroutine.
    protected Coroutine attackCoroutine;                       // Attack coroutine.
    protected Coroutine combatCoroutine;                       // Battle loop coroutine reference.
    protected Vector3 initialPosition;                         // Enemy initial position - used if enemy position has to be reset or if the enemy returns back from outside the enemy group area.
    protected float randomMovementCounter = 0f;                // Random movement counter
    protected float randomMovementFrameChecker;                // Random movement frame checker - used to calculate when a random movement needs to happen.

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {

        if ( isAlive ) {

            // check enemy group movement and interaction area.
            CheckPivotDistance();

            // look at the player if is in range or in battle.
            if ( isLookingAtPlayer ) {
                LookAtPlayer();
            }

            // check for random movement if the enemy is watching.
            if ( currentState == State.watching ) {
                CheckIfRandomMove();
            }
        }
    }

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
        player.GetDamage( attack );
    }

    /// <summary>
    /// Move enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to move</param>
    /// <param name="extraSpeed">float - any extra speed to apply to this movement call. Default to 1f</param>
    /// <param name="newState">State - New state to apply to enemy when the movement coroutine finishes. Default to null.</param>
    public virtual IEnumerator Move( Vector3 destination, float extraSpeed = 1f, State newState = State.none ) {

        // TODO: Replace by rotate method.
        transform.LookAt( destination );
        yield return new WaitForSeconds( .1f );

        float remainingDistance = ( transform.position - destination ).sqrMagnitude;

        while ( remainingDistance > 0.1f ) {
            isMoving = true;
            
            Vector3 newPosition = Vector3.MoveTowards( _rigi.position, destination, ( data.speed * extraSpeed ) * Time.deltaTime );
            _rigi.MovePosition( newPosition );

            remainingDistance = ( transform.position - destination ).sqrMagnitude;

            yield return new WaitForFixedUpdate();
        }

        if ( newState != State.none ) {
            currentState = newState;
        }

        isMoving = false;
        moveCoroutine = null;
    }

    /// <summary>
    /// Rotate enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to look at</param>
    public virtual IEnumerator Rotate( Transform destination ) {

        float rotationSpeed = data.speed * 2f;
        float rotateTime = 0f;

        // get to - from values.
        Quaternion current = transform.rotation;
        transform.LookAt( destination );
        Quaternion final = transform.rotation;
        
        yield return null;


        // reset rotation to start lerping.
        // transform.rotation = current;
        // final = new Quaternion( current.x, final.y, current.z, final.w );

        /*
        if ( anim != null && animBoolVariable != "" ) {
            anim.SetBool( animBoolVariable, true );
        }
        */

        /*
        while ( ! Mathf.Approximately( transform.rotation.y, final.y ) ) {

            isRotating = true;

            rotateTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp( current, final, .5f / rotateTime );

            yield return new WaitForFixedUpdate();
        }

        isRotating = false;

        if ( anim != null && animBoolVariable != "" ) {
            anim.SetBool( animBoolVariable, false );
        }
        */
    }

    /// <summary>
    /// Attack method.
    /// Enemy performs an attack from the
    /// attack list.
    /// </summary>
    /// <return>IEnumerator</return>
    protected abstract IEnumerator Attack();

    /// <summary>
    /// Battle loop.
    /// This loop will be initialised every time 
    /// the enemy enters into combat mode.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected abstract IEnumerator BattleLoop();

    /// <summary>
    /// Look at the player.
    /// </summary>
    private void LookAtPlayer() {
        if ( Player.instance != null ) {
            transform.LookAt( Player.instance.gameObject.transform );
        }
    }


    /// <summary>
    /// Engage enemy in battle.
    /// </summary>
    public void EngageInBattle() {
        currentState = State.battling;
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
    /// Random movement. Usually performed
    /// by enemies in watch state.
    /// </summary>
    public void RandomMovement() {
        float x = UnityEngine.Random.Range( - data.randXMovementAmplitude, data.randXMovementAmplitude );
        float z = UnityEngine.Random.Range( - data.randZMovementAmplitude, data.randZMovementAmplitude );

        Vector3 toMove = new Vector3( transform.position.x + x, transform.position.y, transform.position.z + z );
        
        moveCoroutine = StartCoroutine( Move( toMove ) );
    }

    /// <summary>
    /// Check if a random movement has
    /// to be performed.
    /// </summary>
    private void CheckIfRandomMove() {

        if ( randomMovementCounter < randomMovementFrameChecker ) {
            randomMovementCounter++;
        } else {
            float rand = UnityEngine.Random.Range( 0f, 100f );

            if ( rand < 65f && ! isMoving && moveCoroutine == null ) {
                RandomMovement();
            }

            randomMovementCounter = 0;
            randomMovementFrameChecker = CalculateRandomMovementRatio();
        }
    }

    /// <sumamry>
    /// Calculate random movement ratio.
    /// Data movement ration base is not real
    /// movememnt ratio. To increase randomness and
    /// make the enemy more realistic, random movement
    /// ratio will slightly vary after every time
    /// the enemy performs a random movement.
    /// </summary>
    private float CalculateRandomMovementRatio() {
        // random movement normalized 0 - 100f;
        float ratio = 100f - data.randomMovementRatio;
        ratio = ( UnityEngine.Random.Range( 0f, 50f ) + ratio ) * 2.5f;     // Multiply to add more time between random movement.
        return ratio;
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
    
        if ( isMoving ) {
            StopMoving();
        }

        // disable physics for this enemy - they are not longer neecesary.
        if ( _rigi != null ) {
            _rigi.isKinematic = true;
            _rigi.useGravity = false;
        }

        // remove colliders
        RemoveCollider();

        // disable enemy in the enemy group.
        if ( enemyGroup != null ) {
            enemyGroup.DisableEnemy( publicID );
        }

        // display death particles if required.
        if ( deathParticles ) {
            StartCoroutine( deathParticles.DisplayDeathParticles() );
        }

        // update data defeated value.
        data.defeated++;

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

            if ( colliderType == ColliderType.sphere ) {
                SphereCollider collider = GetComponent<SphereCollider>();
                var childrenColliders = GetComponentsInChildren<SphereCollider>();

                if ( collider != null ) {
                    Destroy( collider );
                }

                if ( childrenColliders != null ) {
                    foreach ( SphereCollider childCollider in childrenColliders ) {
                        Destroy( childCollider );
                    }
                }
            } else if ( colliderType == ColliderType.box ) {

                BoxCollider collider = GetComponent<BoxCollider>();
                var childrenColliders = GetComponentsInChildren<BoxCollider>();

                if ( collider != null ) {
                    Destroy( collider );
                }

                if ( childrenColliders != null ) {
                    foreach ( BoxCollider childCollider in childrenColliders ) {
                        Destroy( childCollider );
                    }
                }
            } else if ( colliderType == ColliderType.capsule ) {

                CapsuleCollider collider = GetComponent<CapsuleCollider>();
                var childrenColliders = GetComponentsInChildren<CapsuleCollider>();

                if ( collider != null ) {
                    Destroy( collider );
                }

                if ( childrenColliders != null ) {
                    foreach ( CapsuleCollider childCollider in childrenColliders ) {
                        Destroy( childCollider );
                    }
                }
            } else if ( colliderType == ColliderType.mesh ) {

                MeshCollider collider = GetComponent<MeshCollider>();
                var childrenColliders = GetComponentsInChildren<MeshCollider>();

                if ( collider != null ) {
                    Destroy( collider );
                }

                if ( childrenColliders != null ) {
                    foreach ( MeshCollider childCollider in childrenColliders ) {
                        Destroy( childCollider );
                    }
                }
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
    /// Check distance within the 
    /// enemy group's pivot
    /// </summary>
    public virtual void CheckPivotDistance() {
        if ( enemyGroup != null ) {
            float distance = Vector3.Distance( this.transform.position, enemyGroup.groupPivot.transform.position );
            
            if ( distance > enemyGroup.maxDistance ) {

                if ( isMoving ) {
                    StopMoving();
                }
                
                // enemy go back to its initial position a little bit faster than base speed.
                moveCoroutine = StartCoroutine( Move( initialPosition, .3f, initialState ) );
            }
        }
    }

    /// <summary>
    /// Set up enemy group.
    /// </summary>
    public virtual void SetEnemyGroup( EnemyGroup enemyGroup ) {
        this.enemyGroup = enemyGroup;
    }

    /// <summary>
    /// Set public id.
    /// </summary>
    /// <param name="publicId">int - public ID</param>
    public virtual void SetPublicId( int publicID ) {
        this.publicID = publicID;
    }

    /// <summary>
    /// Get public id.
    /// </summary>
    /// <returns>int</returns>
    public virtual int GetPublicId() {
        return this.publicID;
    }

    /// <summary>
    /// Set up navigators reference
    /// to this enemy instance.
    /// </summary>
    private void SetUpNavigators() {
        foreach ( EnemyNavigator navigator in navigators ) {
            navigator.SetUpNavigator( this );
        }
    }

    /// <summary>
    /// Get enemy state.
    /// </sumamry>
    /// <returns>State</returns>
    public State GetState() {
        return this.currentState;
    }

    /// <summary>
    /// Set enemy state.
    /// </summary>
    /// <param name="newState">State - new enemy state.</param>
    public void SetState( State newState ) {
        this.currentState = newState;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public virtual void Init() {
        currentHp = data.hp;
        maxHp = data.hp;
        attack = data.attack;
        initialPosition = transform.position;
        initialState = currentState;

        // set up enemy navigatos.
        if ( navigators != null ) {
            SetUpNavigators();
        }

        // set up random movement ratio.
        randomMovementFrameChecker = CalculateRandomMovementRatio();

        // set UI enemy bar component.
        enemyHPBar = GameObject.Find( "EnemyHPBar" ).GetComponent<EnemyHPBar>();

        // get rigibody component reference.
        _rigi = GetComponent<Rigidbody>();

    }

}
