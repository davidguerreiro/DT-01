using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {
    public float speed = 5f;                                // Rotate speed.
    public GameObject pivot;                                // Pivot to rotate around.
    public bool rotateX;                                    // Wheter or not the object rotates in the X axis.
    public bool rotateY;                                    // Wheter or not the object rotates in the Y axis.
    public bool rotateZ;                                    // Wheter or not the object rotates in the Z axis.
    public bool invertDirection = false;                    // Invert rotation.

    private bool _canMove = true;                           // Flag used to stop any movememnt from this script.

    // Update is called once per frame
    void FixedUpdate(){

        if ( _canMove ) {
            RotateObject();
        }
    }

    /// <summary>
    /// Rotate the object around the pivot
    /// </summary>
    private void RotateObject() {
        
        float rotateXValue = ( this.rotateX ) ? 1f : 0f;
        float rotateYValue = ( this.rotateY ) ? 1f : 0f;
        float rotateZValue = ( this.rotateZ ) ? 1f : 0f;

        float angles = ( ! this.invertDirection ) ? speed * Time.deltaTime : - speed * Time.deltaTime;
        Vector3 axis = new Vector3( rotateXValue, rotateYValue, rotateZValue );

        transform.RotateAround( pivot.transform.position, axis, angles );
    }

    /// <summary>
    /// Stop any movement or rotation.
    /// </summary>
    public void Stop() {
        _canMove = false;
    }
}
