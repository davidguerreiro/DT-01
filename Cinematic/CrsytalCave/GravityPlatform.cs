using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPlatform : MonoBehaviour {
    [Header("Status")]
    public bool navigable;                                      // If true, this platform has reached the endpoint and it is navigable.

    [Header("Components")]
    public GameObject platform;                                 // Platform gameObject reference.
    public GameObject endPoint;                                 // EndPoint gameObject reference.]

    [Header("Settings")]
    public float speed;                                         // Platform speed movement when moving towards the end point.

    private Animator _anim;                                     // Platform animator component reference.
    private Rigidbody _rigi;                                    // Rigibody component reference.
    private Vector3 _initLocalPosition;                         // Init local position for platform. Used to restart event.
    private Coroutine _animRoutine;                             // Animation coroutine reference.
    private float _acceleration;                                // Acceleration value.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Move platform to endPoint
    /// coroutine wrapper.
    /// </summary>
    public void MovePlatformToEndPoint() {
        if ( ! navigable && _animRoutine == null ) {
            _animRoutine = StartCoroutine( MovePlatformToEndPointRoutine() );
        }
    }

    /// <summary>
    /// Move platform to endPoint.
    /// </summary>
    public IEnumerator MovePlatformToEndPointRoutine() {
        navigable = true;

        // trigger rotation animation.
        _anim.SetTrigger( "RotateInAnim" );
        float remainingDistance = ( platform.transform.position - endPoint.transform.position ).sqrMagnitude;

        while( remainingDistance > 0.1f ) {
            Vector3 newPosition = Vector3.MoveTowards( _rigi.position, endPoint.transform.position, speed * Time.deltaTime );
            yield return new WaitForFixedUpdate();

            remainingDistance = ( platform.transform.position - endPoint.transform.position ).sqrMagnitude;
        }
    }

    /// <summary>
    /// Reset gravity platform.
    /// </summary>
    public void Reset() {
        platform.transform.position = _initLocalPosition;
        navigable = false;
        _animRoutine = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get inital platform position.
        _initLocalPosition = platform.transform.position;

        // get platform anim component.
        // _anim = platform.GetComponent<Animator>();

        // get rigibody component reference.
        _rigi = platform.GetComponent<Rigidbody>();
    }
}
