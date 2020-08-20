using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLintern : MonoBehaviour {
    public Lintern linternData;                         // Lintern scriptable object reference.
    private Animator _animator;                         // Animator component reference.

    // Start is called before the first frame update
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
            _animator.SetBool( "SwitchOn", true );
        }
    }

    /// <summary>
    /// Switch lintern off.
    /// </summary>
    private void SwitchOff() {
        linternData.enabled = false;
        _animator.SetBool( "SwitchOn", false );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _animator = GetComponent<Animator>();
    }
}
