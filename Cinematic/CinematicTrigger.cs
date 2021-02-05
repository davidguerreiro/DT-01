using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour {
    public Cinematic cinematic;                             // Cinematic to be triggerd by this trigger cinematic.

    [Header("Status")]
    public bool triggered;                                  // Wheter this cinematic has already been triggered.

    [Header("Settings")]
    public bool triggerByContact;                           // Wheter to trigger this cinematic by contact.

    /// <summary>
    /// Trigger cinematic.
    /// </summary>
    public void TriggerCinamatic() {
        cinematic.PlayCinematic();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        if ( triggerByContact && ! triggered && other.tag == "Player" ) {
            TriggerCinamatic();
        }
    }
}
