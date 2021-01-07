using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    protected bool isSpawned = false;                       // Wheter the enemy is spawned in the game scene.
    [SerializeField]
    protected bool isMoving = false;                        // Whether the enemy is moving.
    [SerializeField]
    protected bool isRotating = false;                      // Whether the enemy is rotating.
    [SerializeField]
    protected bool isAttacking = false;                     // Whether the enemy is performing an attack.
    [SerializeField]
    protected bool isChasingPlayer = false;                 // When chasing player, every Move coroutine destination will be overwritten by player position each frame.
    [SerializeField]
    protected bool inBattle = false;                        // Whether the enemy is engaged in battle.
    [SerializeField]
    protected bool isLookingAtPlayer = false;               // Whether the enemy is looking at the player continuosly.
    [SerializeField]
    protected bool onNavMeshPath = false;                   // Whether the enemy is in a navMeshPath loop.

    private const float criticBoost = 1.5f;                 // Boos to multiply critic damage.

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

    [Header("Battle Settings")]
    [SerializeField]
    protected bool ignoreXRotation;                             // Ignore X rotation when facing player.
    [SerializeField]
    protected bool ignoreYRotaion;                              // Ignore Y rotation when facing player.
    [SerializeField]
    protected bool ignoreZRotaion;                              // Ignore Y rotation when facing player.

    [Header("HightLight Effects")]
    [SerializeField]
     protected HighlightPlus.HighlightEffect highlightEffect;   // Highlight effects class.
     [SerializeField]
     protected Color hitColor;                                  // Hit color.
     [SerializeField]
     protected float hitDuration;                               // Hit effect duration.
     [SerializeField]
     protected float hitIntensity;                              // Hit effect intensity.


    // GameObject components.
    protected Rigidbody _rigi;                                  // Rigibody component reference.
    protected NavMeshAgent _agent;                              // NavMeshAgent component reference.

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

    protected Coroutine moveCoroutine;                         // Moving coroutine.
    protected Coroutine rotateCoroutine;                       // Rotating coroutine.
    protected Coroutine attackCoroutine;                       // Attack coroutine.
    protected Coroutine battleCoroutine;                       // Battle loop coroutine reference.
    protected Vector3 initialPosition;                         // Enemy initial position - used if enemy position has to be reset or if the enemy returns back from outside the enemy group area.
    protected float randomMovementCounter = 0f;                // Random movement counter
    protected float randomMovementFrameChecker;                // Random movement frame checker - used to calculate when a random movement needs to happen.

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {

        if ( isAlive && ! GameManager.instance.isPaused ) {

            // Debug.Log( currentState );

            // look at the player if is in range or in battle.
            if ( isLookingAtPlayer ) {
                LookAtPlayer( ignoreXRotation, ignoreYRotaion, ignoreZRotaion );
            }

            // check for random movement if the enemy is watching.
            if ( currentState == State.watching ) {
                CheckIfRandomMove();
            }

            // check enemy group movement and interaction area.
            if ( currentState != State.none && currentState != State.returning ) {
                CheckPivotDistance();
            }
        }
    }

    /// <summary>
    /// Get damage method.
    /// </summary>
    /// <param name="externalImpactValue">float - damage value caused external attacker, usually the player.</param>
    /// <param name="criticRate">float - critic rate value. Default to 0.</param>
    /// <param name="isMelee">bool - Flag to control that the attack received was a melee attack.False by default.</param>
    public virtual void GetDamage( float externalImpactValue, float criticRate = 0f, bool isMelee = false ) {
        if ( isAlive ) {
            // calculate damage base.
            float damageReceived = ( externalImpactValue / data.defense ) + UnityEngine.Random.Range( 0f, .5f );

            // check if critic.
            bool isCritic = GetIfCritic( criticRate );

            // calculate melee resistance.
            if ( isMelee ) {
                damageReceived *= data.meleeVulnerable;
            }

            // calculate critic.
            if ( isCritic ) {
                damageReceived *= criticBoost;
            }

            // update damage.
            currentHp -= damageReceived;
            
            // update related UI components.
            UpdateUI( damageReceived, isCritic );

            if ( currentHp <= 0f ) {
                StartCoroutine( Die() );
            } else {
                // display glowing effect here.
                if ( highlightEffect != null ) {
                    highlightEffect.HitFX( hitColor, hitDuration, hitIntensity );
                }
                // display hit particles.
                if ( hitParticles != null ) {
                    // hitParticles.DisplayHitParticles();
                }
            }
        }
    }

    /// <summary>
    /// Get if critic.
    /// </summary>
    /// <param name="criticRate">float - critic damage rate</param>
    /// <returns>bool</returns>
    private bool GetIfCritic( float criticRate ) {
        return ( 100f - ( 100f - ( criticRate + data.luck ) ) > UnityEngine.Random.Range( 0f, 100f ) );
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
    /// If flag "isChasingPlayer" is true, the enemy will constantly move towards the player because destination
    /// will be replaced by player position, even if the move coroutine has already started.
    /// </summary>
    /// <param name="setDestination">Vector3 - position where the enemy is going to move</param>
    /// <param name="useNavMesh">bool - wheter to move using nav mesh. False by defaul</param>
    /// <param name="extraSpeed">float - any extra speed to apply to this movement call. Default to 1f</param>
    /// <param name="newState">State - New state to apply to enemy when the movement coroutine finishes. Default to null.</param>
    public virtual IEnumerator Move( Vector3 setDestination, bool useNavMesh = false, float extraSpeed = 1f, State newState = State.none ) {
        isMoving = true;
        Vector3 destination = ( isChasingPlayer ) ? Player.instance.transform.position : setDestination;

        // TODO: Replace by rotate method.
        transform.LookAt( destination );
        yield return new WaitForSeconds( .1f );

        if ( useNavMesh && _agent != null ) {
            onNavMeshPath = true;

            // move using navMesh.
            _agent.Warp( transform.position );
            _agent.speed = data.speed * extraSpeed;
            _agent.SetDestination( destination );

            do {
                if ( isChasingPlayer ) {
                    destination = Player.instance.transform.position;
                    _agent.SetDestination( destination );
                }

                // check if agent has reached destination.
                if ( ! _agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance && ( _agent.hasPath || _agent.velocity.sqrMagnitude == 0f ) ) {
                    onNavMeshPath = false;
                }
                yield return new WaitForFixedUpdate();
            } while ( onNavMeshPath );
        } else {
            float remainingDistance = ( transform.position - destination ).sqrMagnitude;

            // move using rigibody and physics engine.
            while ( remainingDistance > 0.1f ) {

                Vector3 newPosition = Vector3.MoveTowards( _rigi.position, destination, ( data.speed * extraSpeed ) * Time.deltaTime );
                _rigi.MovePosition( newPosition );

                // ensure enemy is looking at the new destination.
                transform.LookAt( destination );

                yield return new WaitForFixedUpdate();
                // update destination if required and remaining distance.
                destination = ( isChasingPlayer ) ? Player.instance.transform.position : setDestination;
                remainingDistance = ( transform.position - destination ).sqrMagnitude;
                
            }
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
    /// <param name="ignoreX">bool - whether to ignore X axis False by default</param>
    /// <param name="ignoreY">bool - whether to ignore Y axis False by default</param>
    /// <param name="ignoreZ">bool - whether to ignore Z axis False by default</param>
    private void LookAtPlayer( bool ignoreX = false, bool ignoreY = false, bool ignoreZ  = false ) {
        if ( Player.instance != null ) {
            float x = ( ignoreX ) ? transform.position.x : Player.instance.gameObject.transform.position.x;
            float y = ( ignoreY ) ? transform.position.y : Player.instance.gameObject.transform.position.y;
            float z = ( ignoreZ ) ? transform.position.z : Player.instance.gameObject.transform.position.z;

            transform.LookAt( new Vector3( x, y, z ) );
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
            isMoving = false;
            isChasingPlayer = false;
            
            if ( onNavMeshPath && _agent != null ) {
                _agent.isStopped = true;
            }

            if ( moveCoroutine != null ) {
                StopCoroutine( moveCoroutine );
                moveCoroutine = null;
            }
        }
    }
    

    /// <summary>
    /// Stop battle loop.
    /// </summary>
    public virtual void StopBattle() {

        if ( isLookingAtPlayer ) {
            isLookingAtPlayer = false;
        }

        if ( isChasingPlayer ) {
            isChasingPlayer = false;
        }

        // stop any attack.
        if ( isAttacking ) {
            isAttacking = false;
            
            if ( attackCoroutine != null ) {
                StopCoroutine( attackCoroutine );
                attackCoroutine = null;
            }
        }

        // stop battle loop.
        if ( inBattle  ) {
            inBattle = false;
            
            if ( battleCoroutine != null ) {
                StopCoroutine( battleCoroutine );
                battleCoroutine = null;
            }
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
        
        moveCoroutine = StartCoroutine( Move( toMove, true ) );
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
    /// <param name="damageGot">float - Damage got by the enemy.</param>
    /// <param name="isCritic">bool - whether the damage received is critic. False by default.</param>
    private void UpdateUI( float damageGot, bool isCritic = false ) {
        // Update UI.
        if ( GamePlayUI.instance.enemyDataSection.enemyID != publicID ) {
            if ( ! GamePlayUI.instance.enemyDataSection.displayed) {
                GamePlayUI.instance.enemyDataSection.Display();
            }
            GamePlayUI.instance.enemyDataSection.SetUp( publicID, data.enemyName, data.enemySprite, currentHp, maxHp );
        }

        if ( ! GamePlayUI.instance.enemyDataSection.displayed) {
            GamePlayUI.instance.enemyDataSection.Display();
        }

        GamePlayUI.instance.enemyDataSection.hpBar.UpdateHP( currentHp );
        GamePlayUI.instance.enemyDataSection.ResetBarDisplayedCounter();

        GamePlayUI.instance.damageSection.DisplayDamage( damageGot, isCritic );
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

        if ( inBattle ) {
            StopBattle();
        }

        // disable physics for this enemy - they are not longer neecesary.
        if ( _rigi != null ) {
            _rigi.isKinematic = true;
            _rigi.useGravity = false;
        }

        // remove colliders
        UpdateColliderStatus( false );

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

        // grant player experience.
        Player.instance.weapon.GetExp( data.expGiven );

        // update UI with exp given.
        GamePlayUI.instance.weaponSectionUI.UpdateExpUI( data.expGiven );

        // let animation happen before the loot drops.
        yield return new WaitForSeconds( .5f );
        loot.DropLoot();

        yield return null;
    }

    /// <summary>
    /// Update collider 
    /// enabled value.
    /// </summary>
    /// <param name="newStatus">bool - new collider status.</param>
    private void UpdateColliderStatus( bool newStatus ) {

        foreach ( ColliderType colliderType in colliderTypes ) {

            if ( colliderType == ColliderType.sphere ) {
                SphereCollider collider = GetComponent<SphereCollider>();
                var childrenColliders = GetComponentsInChildren<SphereCollider>();

                if ( collider != null ) {
                    collider.enabled = newStatus;
                }

                if ( childrenColliders != null ) {
                    foreach ( SphereCollider childCollider in childrenColliders ) {
                        childCollider.enabled = newStatus;
                    }
                }
            } else if ( colliderType == ColliderType.box ) {

                BoxCollider collider = GetComponent<BoxCollider>();
                var childrenColliders = GetComponentsInChildren<BoxCollider>();

                if ( collider != null ) {
                    collider.enabled = newStatus;
                }

                if ( childrenColliders != null ) {
                    foreach ( BoxCollider childCollider in childrenColliders ) {
                        childCollider.enabled = newStatus;
                    }
                }
            } else if ( colliderType == ColliderType.capsule ) {

                CapsuleCollider collider = GetComponent<CapsuleCollider>();
                var childrenColliders = GetComponentsInChildren<CapsuleCollider>();

                if ( collider != null ) {
                    collider.enabled = newStatus;
                }

                if ( childrenColliders != null ) {
                    foreach ( CapsuleCollider childCollider in childrenColliders ) {
                        childCollider.enabled = newStatus;
                    }
                }
            } else if ( colliderType == ColliderType.mesh ) {

                MeshCollider collider = GetComponent<MeshCollider>();
                var childrenColliders = GetComponentsInChildren<MeshCollider>();

                if ( collider != null ) {
                    collider.enabled = newStatus;
                }

                if ( childrenColliders != null ) {
                    foreach ( MeshCollider childCollider in childrenColliders ) {
                        childCollider.enabled = newStatus;
                    }
                }
            }
            
        }
    }

    /// <summary>
    /// Contact player.
    /// </summary>
    /// <param name="other">Collider - The other Collider involved in this collision.</param>
    private void ContactPlayer( Collider other ) {
        Player player = other.GetComponent<Player>();
            
        if ( ! player.playerInput.invencible ) {

            if ( player.playerInput.inMelee && data.meleeVulnerable > 0f ) {
                // get damage from melee attack.
                GetDamage( player.playerInput.weapon.plasmaGunData.meleeDamage, player.playerInput.weapon.plasmaGunData.criticRate, true );
            } else {
                // player gets damage.
                DamagePlayer( player );
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">Collider - The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {
            
        // check if player is colliding.
        if ( other.tag == "Player" ) {
            ContactPlayer( other );
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">Collider - The other Collider involved in this collision.</param>
    void OnTriggerStay( Collider other ) {
        
        // check if player is colliding.
        if ( other.tag == "Player" ) {
            ContactPlayer( other );
        }
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other) {
        
        // stop moving when colliding withing a wall ( layer 9 )
        if ( other.gameObject.layer == 9 && isMoving ) {
            // StopMoving();
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
                currentState =  State.returning;

                moveCoroutine = StartCoroutine( Move( initialPosition, true, 1.3f, initialState ) );
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
    /// Play enemy standard sound.
    /// </summary>
    protected abstract void PlayStandardSound();

    /// <summary>
    /// Play enemy base attack sound.
    /// </summary>
    protected abstract void PlayAttackSound();

    /// <summary>
    /// Play enemy death sound.
    /// </summary>
    protected abstract void PlayDeathSound();

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

        // get rigibody component reference.
        _rigi = GetComponent<Rigidbody>();

        // get navmeshagent component reference.
        _agent = GetComponent<NavMeshAgent>();
    }

}
