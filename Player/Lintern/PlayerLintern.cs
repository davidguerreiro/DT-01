using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLintern : MonoBehaviour {
    public Lintern linternData;                         // Lintern scriptable object reference.
    public LinternFocus leftFocus;                      // Lintern left focus object reference.
    public LinternFocus rightFocus;                     // Lintern right focus object reference.

    // Update is called once per frame
    void Update() {
        ListenForUserEvent();
    }

    /// <summary>
    /// Listen for user events
    /// </summary>
    private void ListenForUserEvent() {

        // enable / disable lintern when the user press the T key.
        if ( Input.GetKeyDown( "t" ) ) {

            if ( ! linternData.enabled ) {
                SwitchOn();
            } else {
                SwitchOff();
            }
        }
    }

    /// <summary>
    /// Switch lintern on.
    /// </summary>
    private void SwitchOn() {

        if ( linternData.currentBattery > 0f ) {
            linternData.enabled = true;
            
            leftFocus.SwitchOn();
            rightFocus.SwitchOn();

            // TODO: Add swicht sound effect here.
        }
    }

    /// <summary>
    /// Switch lintern off.
    /// </summary>
    private void SwitchOff() {
        linternData.enabled = false;
        
        leftFocus.SwitchOff();
        rightFocus.SwitchOff();

        // TODO: Add swicth sound effect here.
    }

}
