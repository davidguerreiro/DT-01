using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {
    public Vector3 centerPoint;                             // Point in the middle where the player aim pointer is looking at.
    private Camera _camera;                                 // Private Camera object.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        
        // update center point to shoot.
        UpdateShootHitPoint();
    }

    /// <summary>
    /// Shoot ray used for shooting.
    /// </summary>
    private void UpdateShootHitPoint() {

        RaycastHit hit;

        // calculate point in the middle of the camera rectangle view.
        Vector3 point = new Vector3( _camera.pixelWidth / 2, _camera.pixelHeight / 2, 0f );

        // create ray from camera to center of the camera point.
        Ray ray = _camera.ScreenPointToRay( point );

        // update center point.
        if ( Physics.Raycast( ray, out hit ) ) {
            centerPoint = hit.point;
        } else {
            centerPoint = Vector3.zero;
        }
    }

    /// <summary>
    /// Display shooting indicator.
    /// This method is mostly intended for
    /// testing purposes.
    /// </summary>
    /// <param name="pos">Vector - hit position</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SphereIndicator( Vector3 pos ) {

        // create sphere.
        GameObject sphere = GameObject.CreatePrimitive( PrimitiveType.Sphere );
        sphere.transform.position = pos;

        yield return new WaitForSeconds( 1f );

        Destroy( sphere );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get main camera.
        _camera = GetComponent<Camera>();

        // set default value for centerPoint.
        centerPoint = Vector3.zero;
    }
}
