using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {
    public bool grounded = false;                                       // Flag to check when the player is gounded, that means, it is in contact with a ground suface.
    public bool isMoving = false;                                       // Flag to control whether the player is moving.
    public float speed = 6f;                                            // Movement speed.
    public float gravity = - 9.8f;                                       // Grravity value - character controller cannot be used with rigiBody so gravity needs to be defined.
    private Rigidbody _rigidbody;                                       // Rigibody component reference.
    private CharacterController _charController;                        // Character Controller component reference

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        // MovePlayerRaw();
        MovePlayer();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get rigibody component reference.
        _rigidbody = GetComponent<Rigidbody>();

        // get character component reference.
        _charController = GetComponent<CharacterController>();
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
        float deltaX = Input.GetAxis( "Horizontal" ) * speed;
        float deltaZ = Input.GetAxis( "Vertical" ) * speed;

        Vector3 movement = new Vector3( deltaX, 0f, deltaZ );

        // limit diagonal movement to the same speed as movement along an axis.
        movement = Vector3.ClampMagnitude( movement, speed );

        // use gravity value for vertical movement.
        movement.y = gravity;

        movement *= Time.deltaTime;

        // transform the movement vector from local to global coordinates.
        movement = transform.TransformDirection( movement );
        
        // Tell the character controller to move the player by that vector.
        _charController.Move( movement );

    }
    

}
