using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {
    [Header("Actions / Status")]
    public bool grounded = false;                                       // Flag to check when the player is gounded, that means, it is in contact with a ground suface.
    public bool isMoving = false;                                       // Flag to control whether the player is moving.
    public bool isRunning = false;                                      // Flat to control whether the player is running.

    [Header("Variables")]
    public float speed = 6f;                                            // Movement speed.
    public float runningSpeed = 10f;                                    // Boost to assign to speed when running.
    public float jumpSpeed = 8f;                                        // Jump speed force.
    public float gravity = - 9.8f;                                       // Grravity value - character controller cannot be used with rigiBody so gravity needs to be defined.

    [Header("References")]
    public GameObject groundChecker;                                    // Player's ground checker.
    public MainWeapon weapon;                                           // Player's weapon class.
    [Header("Movement / Direction")]
    public string xDirection;                                           // Player's x direction.
    public string zDirection;                                           // Player's z direction.
    // [HideInInspector]
    public float deltaX;                                                // Defines the distance the player is moving in the X axis.
    [HideInInspector]
    public float deltaZ;                                                // Defines the distance the player is moving in the Z axis.                                    
    private Rigidbody _rigidbody;                                       // Rigibody component reference.
    private CharacterController _charController;                        // Character Controller component reference
    private Coroutine _jump;                                            // Jump corotine.
    private Coroutine _groundCheckerRoutine;                            // Ground checker coroutine.
    private AudioSource _audio;                                         // Audio source component.

    

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {

        // MovePlayerRaw();

        // movement read input.
        MovePlayer();

        // jump action.
        if ( grounded && Input.GetKeyDown( "space" ) ) {
            _jump = StartCoroutine( "Jump" );
        }

        // run action.
        if ( grounded && isMoving && Input.GetKey( "left shift" ) ) {
            this.isRunning = true;
        } else {
            this.isRunning = false;
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
    private void MovePlayer() {
        
        // update speed if running.
        float movementSpeed = ( this.isRunning ) ? runningSpeed : speed;

        deltaX = Input.GetAxis( "Horizontal" ) * movementSpeed;
        deltaZ = Input.GetAxis( "Vertical" ) * movementSpeed;

        // update player's direction.
        UpdatePlayerMovementDirection( deltaX, deltaZ );

        Vector3 movement = new Vector3( deltaX, 0f, deltaZ );

        // update player movement flag.
        if ( movement.magnitude > 0f ) {
            this.isMoving = true;
        } else {
            this.isMoving = false;
        }

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
        _audio.Play( 0 );

        Vector3 movement = Vector3.zero;
        movement.y = jumpSpeed;


        while ( ! grounded ) {
            movement.y -= ( - gravity ) * Time.deltaTime;
            _charController.Move( movement * Time.deltaTime );
            yield return null; 
        }

        _jump = null;
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
    /// Init class method.
    /// </summary>
    private void Init() {

        // get rigibody component reference.
        _rigidbody = GetComponent<Rigidbody>();

        // get character component reference.
        _charController = GetComponent<CharacterController>();

        // get audio source component reference.
        _audio = GetComponent<AudioSource>();

        // set default direction in the player movement direction control variables.
        xDirection = "";
        zDirection = "";
    }

    

}
