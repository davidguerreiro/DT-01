using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public enum RotationAxes {                              // Rotation Axes number - setting association.
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    };

    public RotationAxes axes = RotationAxes.MouseXandY;
    public FPSInput playerController;                       // FPS Input player controller reference.

    [Header("Sensitivity")]
    public float sensitivityHor = 1f;                       // Horizontal sensitivity for rotation.
    public float sensitivityVer = 1f;                       // Vertical sensitivity for rotation.
    public float aimingSensitivityHor = .5f;                // Horizontal sensitivity when aiming with the main weapon.
    public float aimingSensitivityVer = .5f;                // Vertical sensitivity when aiming with the main weapon.

    public float minimunVert = -45f;                        // Minimum angles to look down.
    public float maximunVert = 45f;                         // Maximium angles to look up.

    public float acceleration = 10f;                        // How fast the maximun speed will be reached.
    public float decceleration = 10f;                       // TODO: Delete if not implemented.

    private float _rotationX = 0f;                          // This is used to calculate the rotation X real value for player after appliying rotation constraints.
    private float speed = 0f;                               // Real movement speed calculated after applied acceleration.
    private float _localSensitivityHor;                     // Real Horizontal sensitivity. Applied after player status calculations and other variables.
    private float _localSensitivityVer;                     // Real Vertical sensitiviry. Applied after player status calculations and other variables.

    void Start() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if ( rigidbody != null ) {
            rigidbody.freezeRotation = true;
        }
        
    }

    // Update is called once per frame
    void Update() {

        if ( ! GameManager.instance.isPaused ) {
            UpdatePlayerView();
        }
    }

    /// <summary>
    /// Check rotation axes frame
    /// by frame.
    /// </summary>
    private void UpdatePlayerView() {

        if ( playerController.isAiming ) {
            _localSensitivityHor = aimingSensitivityHor;
            _localSensitivityVer = aimingSensitivityVer;
        } else {
            _localSensitivityHor = sensitivityHor;
            _localSensitivityVer = sensitivityVer;
        }

        if ( axes == RotationAxes.MouseX ) {
            // horizontal rotation here.
            transform.Rotate( 0f, Input.GetAxis( "Mouse X" ) * _localSensitivityHor, 0f );
        }
        else if ( axes == RotationAxes.MouseY ) {
            // vertical rotation here.
            _rotationX -= Input.GetAxis( "Mouse Y" ) * _localSensitivityVer;
            _rotationX = Mathf.Clamp( _rotationX, minimunVert, maximunVert );

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3( _rotationX, rotationY, 0f );
        }
        else {
            // both horizontal and vertical rotation here.

            // calculate vertical rotation.
            _rotationX -= Input.GetAxis( "Mouse Y" ) * _localSensitivityVer;
            _rotationX = Mathf.Clamp( _rotationX, minimunVert, maximunVert );

            // calculate horizontal rotation.
            float delta = Input.GetAxis( "Mouse X" ) * _localSensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3( _rotationX, rotationY, 0f );

        }
    }

    /// <summary>
    /// Get current acceleration.
    /// TODO: Delete if not used.
    /// </summary>
    /// <returns>float</returns>
    private float CalculateAcceleration() {
        return 0f;
    }

}
