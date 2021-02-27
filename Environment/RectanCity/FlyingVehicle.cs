using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingVehicle : MonoBehaviour {

    [Header("Status")]
    public bool canMove;                                // Whether this flying vehicle can move.

    public enum Speed {
        slow,
        normal,
        fast,
    };

    [Header("Settigns")]
    public Speed speed;                                 // Vehicle speed.

    [Header("Components")]
    public GameObject vehicle;                          // Vehicle 3D model gameObject.
    public GameObject origin;                           // Origint position.
    public GameObject destination;                      // Destination position.

    private float _floatSpeed;                           // Float speed used to move the vehicle.

    /// <summary>
    /// Get vehicle speed.
    /// </summary>
    /// <returns>float</returns>
    private float GetSpeed() {
        float floatSpeed = 0;

        switch (speed) {
            case Speed.slow:
                floatSpeed = 30f;
                break;
             case Speed.normal:
                floatSpeed = 60f;
                break;
            case Speed.fast:
                floatSpeed = 90f;
                break;
            default:
                break;
        }

        return floatSpeed;
    }

    
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        if (canMove) {
            MoveVehicle();
        }
    }

    /// <summary>
    /// Get vehicle speed.
    /// </summary>
    /// <returns>float</returns>
    private float GetFloatSpeed() {
        float floatSpeed = 0;

        switch (speed) {
            case Speed.slow:
                floatSpeed = 30f;
                break;
             case Speed.normal:
                floatSpeed = 60f;
                break;
            case Speed.fast:
                floatSpeed = 90f;
                break;
            default:
                break;
        }

        return floatSpeed;
    }

    /// <summary>
    /// Move vehicle towards
    /// destination.
    /// </summary>
    public void MoveVehicle() {
        float distance;

        vehicle.transform.position = Vector3.MoveTowards(vehicle.transform.position, destination.transform.position, _floatSpeed * Time.deltaTime);
        distance = Vector3.Distance(vehicle.transform.position, destination.transform.position);
        Debug.Log(distance);
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        _floatSpeed = GetFloatSpeed();
    }
}
