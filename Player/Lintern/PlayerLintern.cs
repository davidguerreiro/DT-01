using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLintern : MonoBehaviour {
    public Lintern linternData;                         // Lintern scriptable object reference.
    public LinternFocus leftFocus;                      // Lintern left focus object reference.
    public LinternFocus rightFocus;                     // Lintern right focus object reference.
    private AudioComponent _audio;                      // Audio component class reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

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

            _audio.PlaySound();
        }
    }

    /// <summary>
    /// Switch lintern off.
    /// </summary>
    private void SwitchOff() {
        linternData.enabled = false;
        
        leftFocus.SwitchOff();
        rightFocus.SwitchOff();

        _audio.PlaySound();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }

}
