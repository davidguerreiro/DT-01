using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {
    [Header("Actions / Status")]
    public bool grounded = false;                                       // Flag to check when the player is gounded, that means, it is in contact with a ground suface.
    public bool isMoving = false;                                       // Flag to control whether the player is moving.
    public bool isMovingByInput = false;                                // Checks whether the player is being moved by user input.
    public bool isRunning = false;                                      // Flag to control whether the player is running.
    public bool isAiming = false;                                       // Flag to control aiming action status.
    public bool canMove = true;                                         // Whether the player can be moved by user input.
    public bool invencible = false;                                     // Whether the player can take damage from enemies or any other damage input.

    [Header("Variables")]
    public float speed = 6f;                                            // Movement speed.
    public float runningSpeed = 10f;                                    // Boost to assign to speed when running.
    public float aimingSpeed = 3f;                                      // Aiming movement speed.
    public float jumpSpeed = 8f;                                        // Jump speed force.
    public float runningJumpBoost = 2.5f;                               // Boost for jumping while running.                   
    public float gravity = - 9.8f;                                       // Grravity value - character controller cannot be used with rigiBody so gravity needs to be defined.

    [Header("Settings")]
    public float secondsForInvencible = 1f;                             // Seconds the player is invencible after getting damage.

    [Header("References")]
    public GameObject groundChecker;                                    // Player's ground checker.
    public MainWeapon weapon;                                           // Player's weapon class.
    public Animator cameraAnim;                                         // Player's main camera animator component reference.

    [Header("Movement / Direction")]
    public string xDirection;                                           // Player's x direction.
    public string zDirection;                                           // Player's z direction.
    
    [HideInInspector]
    public float deltaX;                                                // Defines the distance the player is moving in the X axis.
    [HideInInspector]
    public float deltaZ;                                                // Defines the distance the player is moving in the Z axis.                                    
    [HideInInspector]
    public bool moveByExternalForces = false;                           // If true, external forces can be applied to the player.
    [HideInInspector]
    public float externalSpeed = 1f;                                    // External speed used for external forces applied to the player.
    [HideInInspector]
    public Vector3 externalForces;                                      // External forces used to move the player by exterior input, like platforms or being pushed by an enemy.
    private float currentGravity;                                       // Gravity to apply to player.
    private Rigidbody _rigidbody;                                       // Rigibody component reference.
    private CharacterController _charController;                        // Character Controller component reference
    private Coroutine _jump;                                            // Jump corotine.
    private Coroutine _groundCheckerRoutine;                            // Ground checker coroutine.
    private AudioComponent _audio;                                      // Audio source component.
    private MouseLook[] _cameraMouseLooks;                              // Main camera mouselook scripts.
    private MouseLook _playerMouseLook;                                 // Player X mouse look component reference.
    private bool _mouseLookSwapt = false;                               // Flag used to control wheter the mouse look component in player has been swapt to children main camera.
    private float invencibleCounter = 0f;                               // Invencible internal counter.

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {

        // update speed if running.
        float movementSpeed = ( this.isRunning ) ? runningSpeed : speed;

        // get movement input from the player.
        deltaX = Input.GetAxis( "Horizontal" ) * movementSpeed;
        deltaZ = Input.GetAxis( "Vertical" ) * movementSpeed;

        isMovingByInput = false;

        // check for player horizontal movement input.
        if ( canMove ) {
            if ( Input.GetKey( "a" ) || Input.GetKey( "d" ) || Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.RightArrow ) ) {
                isMovingByInput = true;
            }

            if ( Input.GetKey( "w" ) || Input.GetKey( "s" ) || Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.DownArrow ) ) {
                isMovingByInput = true;
            }

            if ( isMovingByInput ) {
                // set mouse X axis rotation looker to parent ( player ).
                SwiftMouseLookers( "toParent" );
            }
        }

        // movement read input.
        MovePlayer();

        // jump action.
        if ( grounded && Input.GetKeyDown( "space" ) ) {
            _jump = StartCoroutine( "Jump" );
        }

        // aim action.
        if ( grounded && ! isRunning && Input.GetMouseButton( 1 ) ) {
            Aim();
        } else {
            StopAiming();
        }

        // run action.
        if ( grounded && isMoving && ! isAiming && Input.GetKey( "left shift" ) ) {
            this.isRunning = true;
        } else {
            this.isRunning = false;
        }

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {

        // check invencible counter if the player is invencible.
        if ( invencible ) {
             UpdateInvencible();
        }
    }

    /// <summary>
    /// Move character by user input.
    /// This method uses raw Unity logic.
    /// </summary>
    private void MovePlayerRaw() {

        float deltaX = Input.GetAxis( "Horizontal" ) * speed;
        float deltaZ = Input.GetAxis( "Vertical" ) * speed;

        transform.Translate( deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime );

    }

    /// <summary>
    /// Move character by user input
    /// using Character Controller component.
    /// </summary>
    public void MovePlayer() {
        
        Vector3 movement = new Vector3();
        float movementSpeed = speed;
            
        // update speed base on player status.
        if ( this.isRunning ) {
            movementSpeed = runningSpeed;
        } else if ( this.isAiming ) {
            movementSpeed = aimingSpeed;
        } else {
            movementSpeed = speed;
        }

        // get movement input from the player.
        deltaX = Input.GetAxis( "Horizontal" ) * movementSpeed;
        deltaZ = Input.GetAxis( "Vertical" ) * movementSpeed;

        movement = new Vector3( deltaX, 0f, deltaZ );
        
        if ( moveByExternalForces ) {

            // if moved by external forces, the player input is override.
            movementSpeed = speed * externalSpeed;

            // move player by external forces - used by defaut when player is not moving.
            movement = externalForces * externalSpeed;
        }

        
        // limit diagonal movement to the same speed as movement along an axis.
        movement = Vector3.ClampMagnitude( movement, movementSpeed );

        // update player's direction.
        UpdatePlayerMovementDirection( deltaX, deltaZ );


        // update player movement flag.
        this.isMoving = ( movement.magnitude > 0f ) ? true : false;

        // limit diagonal movement to the same speed as movement along an axis.
        movement = Vector3.ClampMagnitude( movement, movementSpeed );

        // use gravity value for vertical movement.
        if ( _jump == null ) {
            movement.y = gravity;
        }

        movement *= Time.deltaTime;

        // transform the movement vector from local to global coordinates.
        movement = transform.TransformDirection( movement );
        
        // Tell the character controller to move the player by that vector.
        _charController.Move( movement );

    }

    /// <sumamry>
    /// Reset external forces
    /// and external speed.
    /// </summary>
    public void ResetExternalForces() {
        moveByExternalForces = false;
        externalForces = Vector3.zero;
        externalSpeed = 1f;
    }

    /// <summary>
    /// Update player movement
    /// direction.
    /// </summary>
    /// <param name="deltaX">float - movement speed in the X axis.</param>
    /// <param name="deltaZ">float - movement speed in the Z axis.</param>
    private void UpdatePlayerMovementDirection( float deltaX, float deltaZ ) {

        // X axis.
        if ( deltaX > 0f ) {
            xDirection = "right";
        } else if ( deltaX < 0f ) {
            xDirection = "left";
        } else {
            xDirection = "";
        }

        // Z axis.
        if ( deltaZ > 0f ) {
            zDirection = "forward";
        } else if ( deltaZ < 0f ) {
            zDirection = "backward";
        } else {
            zDirection = "";
        }
    }

    /// <summary>
    /// Jump player logic.
    /// </summary>
    private IEnumerator Jump() {
        
        // temporally disable ground checker.
        if ( _groundCheckerRoutine == null ) {
            _groundCheckerRoutine = StartCoroutine( "DisableGrounding" );
        }

        grounded = false;

        // play jump sound.
        _audio.PlaySound( 0 );

        Vector3 movement = Vector3.zero;
        float jumpForce = ( isRunning ) ? jumpSpeed + runningJumpBoost : jumpSpeed;
        movement.y = jumpForce;

        while ( ! grounded ) {
            movement.y -= ( - gravity ) * Time.deltaTime;
            _charController.Move( movement * Time.deltaTime );
            yield return null; 
        }

        _jump = null;
    }

    /// <summary>
    /// Aim player logic.
    /// </summary>
    private void Aim() {
        isAiming = true;

        // camera zoom in animation.
        cameraAnim.SetBool( "ZoomIn", true );

        // TODO: Call main weapon animation.

    }

    /// <summary>
    /// Stop aiming.
    /// </summary>
    private void StopAiming() {
        if ( isAiming ) {

            // camera zoom back animation.
            cameraAnim.SetBool( "ZoomIn", false );

            // TODO: Call main weapon animation.

            isAiming = false; 
        }
    }

    /// <summary>
    /// Disable gound checker just
    /// right after jumping, then
    /// re-enable again to check 
    /// when the player grounded.
    /// </summary>
    private IEnumerator DisableGrounding() {
        groundChecker.SetActive( false );

        yield return new WaitForSeconds( .1f );
        groundChecker.SetActive( true ); 

        _groundCheckerRoutine = null;
    }

    /// <summary>
    /// Swift mouse X components.
    /// This is used to ensure external physics can
    /// move the player indepently of orientation.
    /// </summary>
    /// <param name="where">string - where to set the mouse X component.</param>
    public void SwiftMouseLookers( string where ) {

        if ( where == "toParent" ) {
            // enable mouse look in the parent and disable in children.
            _playerMouseLook.enabled = true;

            foreach ( MouseLook mouseLookComponent in _cameraMouseLooks ) {
                if ( mouseLookComponent.axes == MouseLook.RotationAxes.MouseX && mouseLookComponent.gameObject.name != "Player" ) {
                    mouseLookComponent.enabled = false;
                }
            }

            // TODO: Correct parent orientation to look at the same direction as children.
            _mouseLookSwapt = false;
        }

        if ( where == "toChildren" ) {
            // enable mouse look in the children and disable in the parent.
            _playerMouseLook.enabled = false;

            foreach ( MouseLook mouseLookComponent in _cameraMouseLooks ) {
                if ( mouseLookComponent.axes == MouseLook.RotationAxes.MouseX && mouseLookComponent.gameObject.name != "Player" ) {
                    mouseLookComponent.enabled = true;
                }
            }

            _mouseLookSwapt = true;
        }
    }

    /// <summary>
    /// Update invencible counter.
    /// </summary>
    private void UpdateInvencible() {

        // 60f = 1sec per frame.
        if ( invencibleCounter < ( secondsForInvencible * 60f ) ) {
            invencibleCounter++;
        } else {
            invencibleCounter = 0;
            invencible = false;
        }
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get rigibody component reference.
        _rigidbody = GetComponent<Rigidbody>();

        // get mouse look component reference.
        _playerMouseLook = GetComponent<MouseLook>();

        // get camera mouse look components.
        _cameraMouseLooks = GetComponentsInChildren<MouseLook>();

        // get character component reference.
        _charController = GetComponent<CharacterController>();

        // get audio source component reference.
        _audio = GetComponent<AudioComponent>();

        // set default direction in the player movement direction control variables.
        xDirection = "";
        zDirection = "";

        // set default external forces.
        externalForces = Vector3.zero;
    }
}
