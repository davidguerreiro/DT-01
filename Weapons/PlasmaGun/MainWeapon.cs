using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : MonoBehaviour {
    public FPSInput player;                                     // Player controller class.
    
    [Header( "Weapon Animation" ) ]
    public float idleSpeed = 0.2f;                              // Idle animation speed.
    public float idleWalkingSpeed = 0.7f;                       // Idle animtion walking speed.
    public float idleRunningSpeed = 2f;                         // Idle animation running speed.
    private Animator animator;                                  // Animator component reference.

    // shooting object pool variables.
    [Header( "Shooting Ammo" ) ]
    public GameObject ammoPrefab;                               // Ammo proyectile prefab to instantiate when shooting.
    public GameObject shootingOrigin;                           // Shooting origin - point from where the proyectiles are shoot from the weapon. 
    private static List<GameObject> ammoPool;                   // Ammo pool list of gameObjects - used to save a ready-to-use pool of ammo objects to shoot.
    public int poolSize;                                        // Number of ammo obejcts to keep in the object pool.
    

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
        SetAnimation();
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
                animator.SetFloat( "idleSpeed", idleSpeed );
                break;
            case AnimStates.Walking:
                animator.SetFloat( "idleSpeed", idleWalkingSpeed );
                break;
            case AnimStates.Running:
                animator.SetFloat( "idleSpeed", idleRunningSpeed );
                break;
            default:
                animator.SetFloat( "idleSpeed", idleSpeed );
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

        if ( ammoPool == null ) {
            ammoPool = new List<GameObject>();
        }

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
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component
        animator = GetComponent<Animator>();
    }
    
}
