using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {
    private Camera _camera;                                 // Private Camera object.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
        // check if the player is shooting with the mouse.
        if ( Input.GetMouseButtonDown( 0 ) ) {
            ShootShootingRay();
        }
    }

    /// <summary>
    /// Shoot ray used for shooting.
    /// </summary>
    private void ShootShootingRay() {

        RaycastHit hit;

        // calculate point in the middle of the camera rectangle view.
        Vector3 point = new Vector3( _camera.pixelWidth / 2, _camera.pixelHeight / 2, 0f );

        // create ray from camera to center of the camera point.
        Ray ray = _camera.ScreenPointToRay( point );

        if ( Physics.Raycast( ray, out hit ) ) {
            StartCoroutine( SphereIndicator( hit.point ) );
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
        _camera = GetComponent<Camera>();
    }
}
