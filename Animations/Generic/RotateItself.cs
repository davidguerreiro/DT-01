using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour {
    public float speed = 10f;                                   // Rotate speed.
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    private bool _canMove = true;                               // Flag used to stop movement.

    // Update is called once per frame
    void Update() {

        if ( _canMove && ! GameManager.instance.isPaused ) {
            RotateObject();
        }
    }

    /// <summary>
    /// Rotate object over itself.
    /// </summary>
    private void RotateObject() {

        float rotationValue = speed * Time.deltaTime;
        float rotateXValue = ( this.rotateX ) ? rotationValue : 0f;
        float rotateYValue = ( this.rotateY ) ? rotationValue : 0f;
        float rotateZvalue = ( this.rotateZ ) ? rotationValue : 0f;

        transform.Rotate( new Vector3( rotateXValue, rotateYValue, rotateZvalue ) );
    }

    /// <summary>
    /// Stop rotation.
    /// </summary>
    public void Stop() {
        _canMove = false;
    }
}
