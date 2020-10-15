using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoWorm : Enemy {
    private Animator _anim;                           // Animator component reference.
    private AudioComponent _audio;                    // Audio component reference.

    [Header("Testing")]
    public Transform destinationTest;

    // Start is called before the first frame update
    void Start() {
        Init();

        if ( destinationTest != null ) {
            // Move( new Vector3( destinationTest.position.x, transform.position.y, destinationTest.position.z ) );
            // Rotate( destinationTest );
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        ListenForMovement();
    }

    /// <summary>
    /// Listen for moving state and enable
    /// animation accordingly.
    /// </summary>
    private void ListenForMovement() {
        if ( isMoving ) {
            _anim.SetBool( "IsMoving", true );
        } else {
            _anim.SetBool( "IsMoving", false );
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

        // play damage sound.
        if ( _audio != null ) {
            _audio.PlaySound( 0 );
        }
    }

    /// <summary>
    /// Die method.
    /// </summary>
    public override IEnumerator Die() {
        StartCoroutine( base.Die() );
        
        // play death animation.
        _anim.SetBool( "Die", true );
        yield return new WaitForSeconds( timeToDissapear );

        // remove enemy from the scene.
        RemoveEnemy();
    }

    /// <summary>
    /// Move enemy.
    /// </summary>
    /// <param name="destination">Vector3 - position where the enemy is going to move</param>
    public new void Move( Vector3 destination ) {
        if ( moveCoroutine == null ) {
            moveCoroutine = StartCoroutine( base.Move( destination ) );
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

        // randomize array if required to improve arbritrariety when attacking.
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

        yield return null;

        switch ( attack.attackName ) {
            case "Intimidate":
                _anim.SetTrigger( "Attack" );
                break;
            case "Bite":
                float damageV = ( attack.damage + UnityEngine.Random.Range( 0f, 2f ) );
                base.attack += damageV;
                _anim.SetTrigger( "Attack" );
                _rigi.AddForce( attack.impulse );
                yield return new WaitForSeconds( .5f );
                base.attack -= damageV;
                break;
        }

        yield return new WaitForSeconds( data.attackRatio * 1.5f );
    }

    /// <summary>
    /// Revove enemy from the scene after
    /// dying.
    /// </summary>
    private void RemoveEnemy() {
        Destroy( this.gameObject );
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
