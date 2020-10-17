using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoWorm : Enemy {
    private Animator _anim;                             // Animator component reference.
    private AudioComponent _audio;                      // Audio component reference.
    private float _animSpeed = 1f;                     // Animation speed multiplier. Used to increase / decrease animation speed.

    [Header("Testing")]
    public Transform destinationTest;               

    // Start is called before the first frame update
    void Start() {
        Init();

        if ( destinationTest != null ) {
            Move( new Vector3( destinationTest.position.x, transform.position.y, destinationTest.position.z ) );
            // Rotate( destinationTest );
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {

        if ( isAlive ) {

            // listen for rotation actions and perform animations.
            // ListenForRotation();

            // listen for moving actions and perform animations.
            ListenForMovement();

            // listen for enemy status and perform actions accordingly.
            ListerForCurrentState();
        }
    }

    /// <summary>
    /// Listen for moving state and enable
    /// animations accordingly.
    /// </summary>
    private void ListenForMovement() {
        if ( isMoving ) {
            // increase speed at returning.
            float useAnimSpeed = ( currentState == State.returning ) ? _animSpeed * 1.5f : _animSpeed;

            _anim.SetFloat( "AnimSpeed", useAnimSpeed );
            _anim.SetBool( "IsMoving", true );
        } else {
            _anim.SetBool( "IsMoving", false );
        }
    }

    /// <summary>
    /// Listen for rotation state and enable
    /// animations accordingly.
    /// </summary>
    private void ListenForRotation() {
        if ( ! isMoving ) {
            if ( isRotating || isLookingAtPlayer ) {
                _anim.SetFloat( "AnimSpeed", _animSpeed * .5f );
                _anim.SetBool( "IsMoving", true );
            } else {
                _anim.SetBool( "IsMoving", false );
            }
        }
    }

    /// <summary>
    /// Check enemy state
    /// and perform actions based
    /// on that.
    /// </summary>
    private void ListerForCurrentState() {

        switch ( currentState ) {
            case State.battling:        // Engage enemy in combat

                if ( ! inBattle && battleCoroutine == null ) {
                    // Debug.Log( "enter battle" );
                    battleCoroutine = StartCoroutine( "BattleLoop" );
                }
                break;
            case State.returning:       // Return to initial position after leaving group area. Any battle the enemy is engaged must end.

                if ( inBattle || battleCoroutine != null ) {
                    StopBattle();
                }
                break;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {
        if ( other.gameObject.tag == "PlayerProjectile" ) {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            GetDamage( bullet.damage );
        }
    }

    /// <summary>
    /// Get damage.
    /// </summary>
    /// <param name="externalImpactValue">float - damage value caused external attacker, usually the player.</param>
    public override void GetDamage( float externalImpactValue ) {
        base.GetDamage( externalImpactValue );
    }

    /// <summary>
    /// Die method.
    /// </summary>
    public override IEnumerator Die() {
        StartCoroutine( base.Die() );
        
        // play death animation.
        PlayDeathSound();
        _anim.SetBool( "Die", true );
        yield return new WaitForSeconds( timeToDissapear );

        // remove enemy from the scene.
        RemoveEnemy();
    }

    /// <summary>
    /// Move enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to move</param>
    /// <param name="extraSpeed">float - extra speed multiplier.</param>
    public new void Move( Vector3 destination, float extraSpeed = 1f ) {
        if ( moveCoroutine == null ) {
            moveCoroutine = StartCoroutine( base.Move( destination, extraSpeed ) );
        }
    }

    /// <summary>
    /// Rotate enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to look at</param>
    public new void Rotate( Transform destination ) {
        if ( rotateCoroutine == null ) {
            rotateCoroutine = StartCoroutine( base.Rotate( destination ) );
        }
    }

    /// <summary>
    /// Attack method.
    /// Enemy performs an attack from the
    /// attack list.
    /// </summary>
    /// <return>IEnumerator</return>
    protected override IEnumerator Attack() {
        isAttacking = true;

        EnemyAttack attack = null;

        float damage = data.attack;
        var attacks = data.attacks;

        // randomize array if required to improve randomness when attacking.
        if ( attacks.Length > 1 && UnityEngine.Random.Range( 0, 2 ) == 0 ) {
            Utils.instance.Randomize( attacks );
        }

        // choose an attack to perform.
        do {
            // set an attack to be performed.
            int attackKey = UnityEngine.Random.Range( 0, attacks.Length );
            EnemyData.Actions action = attacks[ attackKey ];

            // check attack ratio and assign attack if performed.
            float chance = 100f - action.rate;
            if ( chance < UnityEngine.Random.Range( 0f, 101f ) ) {
                attack = action.attack;
            }
               
        } while ( attack == null );

        switch ( attack.attackName ) {
            case "Intimidate":
                PlayAttackSound();
                _anim.SetFloat( "AnimSpeed", .35f );
                _anim.SetTrigger( "Attack" );
                _rigi.AddForce( Vector3.up * attack.impulse.y );
                yield return new WaitForSeconds( 1.1f );
                break;
            case "Bite":

                if ( isMoving ) {
                    StopMoving();
                    yield return new WaitForSeconds( .1f );
                }

                // chase player until the enemy is close enough to attack.
                isChasingPlayer = true;
                this.Move( Player.instance.transform.position );
                do {
                    yield return new WaitForFixedUpdate();
                } while ( Vector3.Distance( transform.position, Player.instance.gameObject.transform.position ) > 2.5f );

                StopMoving();
                
                isLookingAtPlayer = false;
                isChasingPlayer = false;
                yield return new WaitForSeconds( .1f );

                float damageV = ( attack.damage + UnityEngine.Random.Range( 0f, 2f ) );
                base.attack += damageV;

                PlayAttackSound();
                _anim.SetTrigger( "Attack" );
                _rigi.AddRelativeForce( attack.impulse );
                

                yield return new WaitForSeconds( .5f );
                base.attack -= damageV;
                
                break;
        }

        // yield return new WaitForSeconds( data.attackRatio * 1.5f );
        // Implement attack ratio algorithim here.
        yield return new WaitForSeconds( 1f );
        _anim.SetFloat( "AnimSpeed", 1f );
        isAttacking = false;
        attackCoroutine = null;
    }

    /// <summary>
    /// Battle loop.
    /// This loop will be initialised every time 
    /// the enemy enters into combat mode.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator BattleLoop() {
        inBattle = true;
        int decision = 0;

        if ( isMoving ) {
            StopMoving();
        }

        // alert other members of the group about the player presence.
        if ( enemyGroup != null ) {
            enemyGroup.AlertEnemies();
        }

        PlayStandardSound();

        while ( currentState == State.battling && isAlive ) {

            if ( isMoving ) {
                StopMoving();
            }

            // look at player.
            isLookingAtPlayer = true;
            // Debug.Log( "reloop" );
            yield return new WaitForSeconds( Random.Range( .5f, 2.5f ) );

            // decide action to perform.
            // - 0, 1: Random movement.
            // - 2,3,4 : Perform attack.
            // - 5: Do nothing.
            decision = Random.Range( 2, 6 );

            // random movement - disabled at the moment.
            if ( decision < 2 ) {
                isLookingAtPlayer = false;
                RandomMovement();
                yield return new WaitForSeconds( .1f );
                
                // wait till movement is complete.
                do {
                    yield return new WaitForFixedUpdate();
                } while ( isMoving && moveCoroutine != null );

            } else if ( decision < 5 ) {
                // perform attack.
                attackCoroutine = StartCoroutine( Attack() );
                yield return new WaitForSeconds( .1f );

                // wait till attack is performed.
                do {
                    yield return new WaitForFixedUpdate();
                    
                } while ( isAttacking && attackCoroutine != null );

            } else {
                // do nothing.
                yield return new WaitForSeconds( .5f );
            }

            
        }

        inBattle = false;
        isLookingAtPlayer = false;
        battleCoroutine = null;
    }

    /// <summary>
    /// Remove enemy from the scene after
    /// dying.
    /// </summary>
    private void RemoveEnemy() {
        Destroy( this.gameObject );
    }

    /// <summary>
    /// Play enemy standard sound.
    /// </summary>
    protected override void PlayStandardSound() {
        if ( _audio != null ) {
            _audio.PlaySound( 0 );
        }
    }

    /// <summary>
    /// Play enemy base attack sound.
    /// </summary>
    protected override void PlayAttackSound() {
        if ( _audio != null ) {
            _audio.PlaySound( 1 );
        }
    }

    /// <summary>
    /// Play enemy death sound.
    /// </summary>
    protected override void PlayDeathSound() {
        if ( _audio != null ) {
            _audio.PlaySound( 2 );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public override void Init() {
        base.Init();

        // get animator component reference.
        _anim = GetComponent<Animator>();

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
