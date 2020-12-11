using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainWeapon : MonoBehaviour {
    // TODO: Fix shooting when moving has deviation for bullets.
    // TODO: Add heated shoot not possible to perform sound or animation.
    public FPSInput player;                                     // Player controller class.

    [Header("Shooting") ]
    public float shootForce;                                    // Shoot force which determines the speed of the projectile.
    public PlasmaGun plasmaGunData;                             // Plasma gun scriptable object.
    public float framesToCharge;                                // How many frames to wait till the system checks for holding mouse button to charge shoot.
    private RayShooter _rayShooter;                             // Component used to aim when shooting.
    
    [Header( "Weapon Animation" ) ]
    public float idleSpeed = 0.2f;                              // Idle animation speed.
    public float idleWalkingSpeed = 0.7f;                       // Idle animtion walking speed.
    public float idleRunningSpeed = 2f;                         // Idle animation running speed.
    public Animator heatedLight;                                // Gun light used for heating input.
    public ParticleSystem[] heatedSmokeParticles;               // Heated smoke particle effects.
    public AudioComponent heatedAudio;                          // Heated audio component class reference.
    private Animator _animator;                                  // Animator component reference.

    // shooting object pool variables.
    [Header( "Shooting Ammo" ) ]
    public GameObject ammoPrefab;                               // Ammo proyectile prefab to instantiate when shooting.
    public GameObject chargedAmmoPrefab;                        // Charged ammo prefab proyectile to instantiate when charging shoot.
    public GameObject shootingOrigin;                           // Shooting origin - point from where the proyectiles are shoot from the weapon. 
    private static List<GameObject> ammoPool;                   // Ammo pool list of gameObjects - used to save a ready-to-use pool of ammo objects to shoot.
    private static List<GameObject> chargedAmmoPool;            // Charged ammo pool list of gameObjects - used to save a ready-to-use poll of ammo charged objects to shoot.
    public int poolSize;                                        // Number of ammo objects to keep in the object pool.
    public int chargedPoolSize;                                 // Number of ammo objects to keep in the charged ammo object pool.
    public float freeAiminDistance = 50f;                       // Distance used to calculate where to shoot when no middle screen hit point is set.                                 

    [Header( "Shooting Particle Effects")]
    public ParticleSystem shootingParticles;                    // Shooting particle system for shooting effect.
    public ParticleSystem smoke;                                // Shooting particle system smoke.
    
    private Camera _mainCamera;                                  // Main camera component - used to calculate middle of the point when the player is not shooting at any object with a collider attached.
    private float _heatedThreshold;                              // Threshold used to calculate when the weapoin is heated or not.

    private AudioComponent _audio;                               // Audio component reference.
    private Coroutine _heatedRoutine;                            // Heated coroutine component reference.
    private Coroutine _chargedShootRoutine;                      // Charged shoot coroutine reference.
    

    [HideInInspector]
    public enum AnimStates {                                    // Machine states for the animation.
        Stopped,
        Walking,
        Running,
    }

    [HideInInspector]
    public AnimStates animStates;                              // Instance for animation states.
    private float _framesHolding;                               // Frames holding the shooting button to charge shoot.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        // set ammo pool for player projectiles shooting.
       SetAmmoPool();
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {

        if ( ! GameManager.instance.isPaused ) {

            // set the weapon animation.
            // SetAnimation();
            
            // calculate point in the screen where the bullet is going to be shot at.
            UpdateShootingOriginPosition();

            // check if weapon has cooled.
            CheckHeatedStatus();

            // check if the user is clicking the left button mouse to shoot.
            if ( Input.GetMouseButtonDown( 0 ) && ! plasmaGunData.heated && ! player.isRunning ) {
                Shoot();
            }

            // check if the user is holding the left button mouse to charge a shoot.
            /*
            if ( Input.GetMouseButton( 0 ) && _chargedShootRoutine == null && ! plasmaGunData.heated && ! player.isRunning && ! player.isAiming ) {
                CheckForStartingChargingShoot();
            }
            */

            // display no ammo sound if the users tries to shoot and the plasma gun is heated.
            if ( Input.GetMouseButtonDown( 0 ) && plasmaGunData.heated ) {

                // display no ammo sound.
                _audio.PlaySound( 1 );
            }

            if ( Input.GetMouseButtonUp( 0 ) ) {
                _framesHolding = 0;
            }
        } else {
            // stop heated sound if game gets paused.
            if ( _heatedRoutine != null ) {
                heatedAudio.StopAudio();
                _heatedRoutine = null;
            }
        }

        Debug.Log( plasmaGunData.currentExp );
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        // check if the user is holding the left button mouse to charge a shoot.
        if ( Input.GetMouseButton( 0 ) && _chargedShootRoutine == null && ! plasmaGunData.heated && ! player.isRunning && ! player.isAiming ) {
            CheckForStartingChargingShoot();
        }
    }

    /// <summary>
    /// Set weapon animation.
    /// </summary>
    public void SetAnimation() {

        // detect player status to define animation.
        if ( player.grounded ) {

            if ( player.isMoving ) {
                animStates = AnimStates.Walking;

                if ( player.isRunning ) {
                    animStates = AnimStates.Running;
                }
            } else {
                animStates = AnimStates.Stopped;
            }
        } else {
            animStates = AnimStates.Stopped;
        }


        // trigger animation.
        switch( animStates ) {
            case AnimStates.Stopped:
                _animator.SetFloat( "idleSpeed", idleSpeed );
                break;
            case AnimStates.Walking:
                _animator.SetFloat( "idleSpeed", idleWalkingSpeed );
                break;
            case AnimStates.Running:
                _animator.SetFloat( "idleSpeed", idleRunningSpeed );
                break;
            default:
                _animator.SetFloat( "idleSpeed", idleSpeed );
                break;
        }

    }

    /// <summary>
    /// Check for starting 
    /// charging shoot.
    /// </summary>
    private void CheckForStartingChargingShoot() {
        if ( _chargedShootRoutine == null && ! plasmaGunData.heated && ! player.isRunning && ! player.isAiming ) {
            if ( _framesHolding < framesToCharge ) {
                _framesHolding++;
            } else {
                _framesHolding = 0;
                _chargedShootRoutine = StartCoroutine( ShootCharged() );
            }
        }
    }

    /// <summary>
    /// Create ammo pool.
    /// Object pool approach is being taken
    /// to shoot proyectiles when shooting the 
    /// weapon, so we avoid instantiate / destroy
    /// objects operations and improve performance.
    /// </summary>
    private void SetAmmoPool() {

        ammoPool = new List<GameObject>();
        chargedAmmoPool = new List<GameObject>();

        // set up standard bullets.
        for ( int i = 0; i < poolSize; i++ ) {

            // set up standard bullets.
            GameObject ammoObject = Instantiate( ammoPrefab );
            ammoObject.transform.parent = shootingOrigin.transform;
            ammoObject.transform.localPosition = Vector3.zero;

            ammoObject.SetActive( false );
            ammoPool.Add( ammoObject );
        }

        // set up charged bullets.
        for ( int i = 0; i < chargedPoolSize; i++ ) {

            // set up charged bullets.
            GameObject chargedAmmoObject = Instantiate( chargedAmmoPrefab );
            chargedAmmoObject.transform.parent = shootingOrigin.transform;
            chargedAmmoObject.transform.localPosition = Vector3.zero;

            chargedAmmoObject.SetActive( false );
            chargedAmmoPool.Add( chargedAmmoObject );
        }
    }

    /// <summary>
    /// Spawn ammo.
    /// </summary>
    /// <param name="ammoType">string - ammo type to spawn. "standard" by default.</param>
    /// <returns>GameObject</returns>
    public GameObject SpawnAmmo( string ammoType = "standard" ) {
        var ammoList = ammoPool;

        switch ( ammoType ) {
            case "standard":
                ammoList = ammoPool;
                break;
            case "charged":
                ammoList = chargedAmmoPool;
                break;
            default:
                ammoList = ammoPool;
                break;
        };

        foreach ( GameObject ammo in ammoList ) {
                
            if ( ammo.activeSelf == false ) {

                // check if it is a already shot projectile.
                if ( ammo.transform.parent == null ) {
                    ammo.transform.parent = shootingOrigin.transform;
                    ammo.transform.localPosition = Vector3.zero;
                }

                ammo.SetActive( true );

                return ammo;
            }

        }

        return null;
    }

    /// <summary>
    /// Shoot proyectiles through
    /// the main weapon.
    /// </summary>
    private void Shoot() {

        GameObject ammo = SpawnAmmo();

        if ( ammo != null ) {

            Vector3 destination = _rayShooter.centerPoint;
            Bullet bullet = ammo.GetComponent<Bullet>();

            // old code do not remove yet.
            //if ( destination.magnitude != 0f ) {
              //  ammo.GetComponent<Bullet>().ShootBullet( destination, shootForce );
            //} else {q
            
            // set bullet direction
            Vector3 aimSpot = _mainCamera.gameObject.transform.position;
            aimSpot += _mainCamera.gameObject.transform.forward * freeAiminDistance;
            
            // adjust bullet direction if the player is moving or running.
            if ( player.xDirection != "" ) {
                aimSpot = AdjustDestination( aimSpot );
            } 

            // set bullet damage
            bullet.damage = plasmaGunData.baseDamage;
            bullet.criticRate = plasmaGunData.criticRate;
            bullet.ShootBullet( aimSpot, shootForce, player );

            // update plasma gun data.
            plasmaGunData.UpdatePlasma();

            // display shooting sound.
            _audio.PlaySound( 0 );

            // play shooting animation.
            player.playerAnim.SetTrigger( "Shoot" );

            // display shooting particle effect.
            smoke.Play();

        }
    }

    /// <summary>
    /// Shoot charged proyectile
    /// throught the main weapon.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator ShootCharged() {
        GameObject ammo = SpawnAmmo( "charged" );

        if ( ammo != null ) {

            Vector3 destination = _rayShooter.centerPoint;
            ChargedBullet bullet = ammo.GetComponent<ChargedBullet>();
            
            // set bullet direction
            Vector3 aimSpot = _mainCamera.gameObject.transform.position;
            
            // adjust bullet direction if the player is moving or running.
            if ( player.xDirection != "" ) {
                aimSpot = AdjustDestination( aimSpot );
            } 

            // set bullet damage.
            bullet.damage = plasmaGunData.GetChargedDamageBaseValue();
            bullet.criticRate = plasmaGunData.criticRate;

            // start charged animation.
            bullet.ChargeShoot();

            player.isCharging = true;

            // wait until user release mouse button.
            do {
                aimSpot = _mainCamera.gameObject.transform.position;
                aimSpot += _mainCamera.gameObject.transform.forward * freeAiminDistance;
                yield return new WaitForFixedUpdate();
            } while ( Input.GetMouseButton( 0 ) );

            player.isCharging = false;

            if ( bullet.readyToShoot ) {

                bullet.ShootBullet( aimSpot, shootForce, player );
                bullet.PlayShootAnim();

                // update plasma gun data.
                plasmaGunData.UpdatePlasma( true );

                // play shooting animation.
                player.playerAnim.SetTrigger( "Shoot" );

                // display shooting particle effect.
                smoke.Play();
            } else {
                bullet.RestoreBullet();
                bullet.StopCharging();
                bullet.ResetChargedBullet();

                // shoot standard bullet.
                Shoot();
            }

            _chargedShootRoutine = null;
        }
    }

    /// <summary>
    /// Adjust aim point when shooting to
    /// when the player is moving or running
    /// to fix speed variation.
    /// </summary>
    /// <param name="destination">Vector3 - point in the world where the bullet is going to be shot</param>
    /// <returns>Vector3</returns>
    private Vector3 AdjustDestination( Vector3 destination ) {

        float desviation = ( player.isRunning ) ? 12f : 10f;                    // Bullet desviation when moving or running.

        if ( player.xDirection == "right" ) {
            destination += _mainCamera.gameObject.transform.right * desviation;
        } else {

            if ( player.isRunning ) {
                desviation += 10f;
            }

            destination += _mainCamera.gameObject.transform.right * ( - desviation );
        }

        return destination;
    }

    /// <summary>
    /// Update shoot origin position.
    /// This is done to ensure we always shoot
    /// through the middle of the screen.
    /// </summary>
    private void UpdateShootingOriginPosition() {

        if ( _rayShooter.centerPoint.magnitude > 0f ) {
            shootingOrigin.transform.LookAt( _rayShooter.centerPoint );
        } else {

            Vector3 aimSpot = _mainCamera.gameObject.transform.position;
            aimSpot += _mainCamera.gameObject.transform.forward * freeAiminDistance;

            shootingOrigin.transform.LookAt( aimSpot );
        }

    }

    /// <summary>
    /// Check if the weapon is
    /// heated.
    /// </summary>
    private void CheckHeatedStatus() {
        if ( plasmaGunData.plasma >= _heatedThreshold ) {
            plasmaGunData.heated = false;

            // remove heated light.
            heatedLight.SetBool( "heatedLight", false );
        } else {

            // enable heated light.
            heatedLight.SetBool( "heatedLight", true );
        }

        if ( plasmaGunData.plasma == 0f && _heatedRoutine == null ) {
            _heatedRoutine = StartCoroutine( PlayHeatedAnim() );
        }
    }

    /// <summary>
    /// Play heated animation.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlayHeatedAnim() {
        float downVolumeSpeed = 0.5f;

        // display smoke particles.
        foreach ( ParticleSystem smokeParticle in heatedSmokeParticles ) {
            smokeParticle.Play();
        }

        // play audio.
        if ( ! heatedAudio.onFade ) {
            heatedAudio.PlaySound();
            yield return new WaitForSeconds( 2f );
            StartCoroutine( heatedAudio.FadeOutSongRoutine( downVolumeSpeed ) );
        }

        _heatedRoutine = null;        
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component
        // _animator = GetComponent<Animator>();

        // get camera component from parent.
        _mainCamera = GetComponentInParent<Camera>();

        // get ray shooter component from camera.
        _rayShooter = GetComponentInParent<RayShooter>();

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();

        // calculate heated threshold - this is used to check and update heated weapon status.
        if ( plasmaGunData != null ) {
            float temp = (float) plasmaGunData.heatedRechargeThreeshold / (float) plasmaGunData.maxPlasma;
            _heatedThreshold = temp * 100f;
        }

        // set next level data if not set ( usually when the game is initialised for the first time );
        if ( plasmaGunData.nextLevel == null ) {
            plasmaGunData.nextLevel = plasmaGunData.GetLevelDataObject( plasmaGunData.level + 1 );
        } 
    }
    
}
