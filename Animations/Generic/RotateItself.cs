using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour {
    public float speed = 10f;                                   // Rotate speed.
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    // Update is called once per frame
    void Update() {
        RotateObject();
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
}
