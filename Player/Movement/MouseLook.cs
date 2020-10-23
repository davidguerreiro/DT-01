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
    public float sensitivityHor = 9f;                       // Horizontal sensitivity for rotation.
    public float sensitivityVer = 9f;                       // Vertical sensitivity for rotation.

    public float minimunVert = -45f;                        // Minimum angles to look down.
    public float maximunVert = 45f;                         // Maximium angles to look up.

    public float acceleration = 10f;                        // How fast the maximun speed will be reached.
    public float decceleration = 10f;

    private float _rotationX = 0f;                          // This is used to calculate the rotation X real value for player after appliying rotation constraints.
    private float speed = 0f;                               // Real movement speed calculated after applied acceleration.

    void Start() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if ( rigidbody != null ) {
            rigidbody.freezeRotation = true;
        }
        
    }

    // Update is called once per frame
    void Update() {
        UpdatePlayerView();
    }

    /// <summary>
    /// Check rotation axes frame
    /// by frame.
    /// </summary>
    private void UpdatePlayerView() {

        if ( axes == RotationAxes.MouseX ) {
            // horizontal rotation here.
            // Debug.Log( Input.GetAxis( "Mouse X" ) );

            transform.Rotate( 0f, Input.GetAxis( "Mouse X" ) * sensitivityHor, 0f );
        }
        else if ( axes == RotationAxes.MouseY ) {
            // vertical rotation here.
            _rotationX -= Input.GetAxis( "Mouse Y" ) * sensitivityVer;
            _rotationX = Mathf.Clamp( _rotationX, minimunVert, maximunVert );

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3( _rotationX, rotationY, 0f );
        }
        else {
            // both horizontal and vertical rotation here.

            // calculate vertical rotation.
            _rotationX -= Input.GetAxis( "Mouse Y" ) * sensitivityVer;
            _rotationX = Mathf.Clamp( _rotationX, minimunVert, maximunVert );

            // calculate horizontal rotation.
            float delta = Input.GetAxis( "Mouse X" ) * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3( _rotationX, rotationY, 0f );

        }
    }

    /// <summary>
    /// Get current acceleration.
    /// </summary>
    /// <returns>float</returns>
    private float CalculateAcceleration() {
        return 0f;
    }

}
