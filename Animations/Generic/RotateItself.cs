using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour {
    public float speed = 10f;                                   // Rotate speed.
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;
    private Vector3 rotation;                                   // Rotation vector.

    // Start is called before the first frame update
    void Start() {
        // Init();
    }

    // Update is called once per frame
    void Update() {
        RotateObject();
    }

    /// <summary>
    /// Rotate object over itself.
    /// </summary>
    private void RotateObject() {
        rotation = transform.rotation.eulerAngles;

        float rotationValue = speed * Time.deltaTime;

        if ( this.rotateX ) {
            rotation = new Vector3( rotation.x += rotationValue, rotation.y, rotation.z );
        }

        if ( this.rotateY ) {
            rotation = new Vector3( rotation.x, rotation.y += rotationValue, rotation.z );
        }

        if ( this.rotateZ ) {
            rotation = new Vector3( rotation.x, rotation.y, rotation.z += rotationValue );
        }

        transform.localEulerAngles = rotation;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        
        // init rotation vector.
        rotation = Vector3.zero;
    }
}
