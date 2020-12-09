using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchForcedArea : MonoBehaviour {

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        if ( other.gameObject.tag == "Player" ) {
            Player.onForcedCrouch = true;
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other) {
        if ( other.gameObject.tag == "Player" ) {
            Player.onForcedCrouch = false;
        }
    }
}
