using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    private FPSInput _playerInput;                                   // Player input class.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get player input script component reference.
        _playerInput = GetComponentInParent<FPSInput>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {

        if ( other.gameObject.layer == LayerMask.NameToLayer( "Ground" ) ) {
            _playerInput.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if ( other.gameObject.layer == LayerMask.NameToLayer( "Ground" ) ) {
            _playerInput.grounded = false;
        }
    }
}
