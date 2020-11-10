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
    private RayShooter _rayShooter;                             // Component used to aim when shooting.
    
    [Header( "Weapon Animation" ) ]
    public float idleSpeed = 0.2f;                              // Idle animation speed.
    public float idleWalkingSpeed = 0.7f;                       // Idle animtion walking speed.
    public float idleRunningSpeed = 2f;                         // Idle animation running speed.
    public Animator heatedLight;                                // Gun light used for heating input.
    private Animator _animator;                                  // Animator component reference.

    // shooting object pool variables.
    [Header( "Shooting Ammo" ) ]
    public GameObject ammoPrefab;                               // Ammo proyectile prefab to instantiate when shooting.
    public GameObject shootingOrigin;                           // Shooting origin - point from where the proyectiles are shoot from the weapon. 
    private static List<GameObject> ammoPool;                   // Ammo pool list of gameObjects - used to save a ready-to-use pool of ammo objects to shoot.
    public int poolSize;                                        // Number of ammo obejcts to keep in the object pool.
    public float freeAiminDistance = 50f;                       // Distance used to calculate where to shoot when no middle screen hit point is set.                                 

    [Header( "Shooting Particle Effects")]
    public ParticleSystem shootingParticles;                    // Shooting particle system for shooting effect.
    public ParticleSystem smoke;                                // Shooting particle system smoke.
    
    private Camera _mainCamera;                                  // Main camera component - used to calculate middle of the point when the player is not shooting at any object with a collider attached.
    private float _heatedThreshold;                              // Threshold used to calculate when the weapoin is heated or not.

    private AudioComponent _audio;                               // Audio component reference.
    

    [HideInInspector]
    public enum AnimStates {                                    // Machine states for the animation.
        Stopped,
        Walking,
        Running,
    }

    [HideInInspector]
    public AnimStates animStates;                              // Instance for animation states.

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
            SetAnimation();
            
            // calculate point in the screen where the bullet is going to be shot at.
            UpdateShootingOriginPosition();

            // check if weapon has cooled.
            CheckHeatedStatus();

            // check if the user is clicking the left button mouse to shoot.
            if ( Input.GetMouseButtonDown( 0 ) && ! plasmaGunData.heated ) {
                Shoot();
            }

            // display no ammo sound if the users tries to shoot and the plasma gun is heated.
            if ( Input.GetMouseButtonDown( 0 ) && plasmaGunData.heated ) {

                // display no ammo sound.
                _audio.PlaySound( 1 );
            }
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
    /// Create ammo pool.
    /// Object pool approach is being taken
    /// to shoot proyectiles when shooting the 
    /// weapon, so we avoid instantiate / destroy
    /// objects operations and improve performance.
    /// </summary>
    private void SetAmmoPool() {

        ammoPool = new List<GameObject>();

        for ( int i = 0; i < poolSize; i++ ) {

            GameObject ammoObject = Instantiate( ammoPrefab );
            ammoObject.transform.parent = shootingOrigin.transform;
            ammoObject.transform.localPosition = Vector3.zero;

            ammoObject.SetActive( false );
            ammoPool.Add( ammoObject );
        }
    }

    /// <summary>
    /// Spawn ammo.
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject SpawnAmmo() {

        foreach ( GameObject ammo in ammoPool ) {
            
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
            bullet.ShootBullet( aimSpot, shootForce, player );
            //}

            // update plasma gun data.
            plasmaGunData.UpdatePlasma();

            // display shooting sound.
            _audio.PlaySound( 0 );

            // display shooting animation.
            _animator.SetTrigger( "baseShooting" );

            // display shooting particle effect.
            smoke.Play();

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
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component
        _animator = GetComponent<Animator>();

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
    }
    
}
