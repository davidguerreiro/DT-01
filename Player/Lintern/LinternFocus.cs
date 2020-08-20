using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinternFocus : MonoBehaviour {
    private Animator _animator;                          // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Switch on the light focus.
    /// </summary>
    public void SwitchOn() {
        _animator.SetBool( "SwitchOn", true );
    }

    /// <summary>
    /// Switch off the light focus.
    /// </summary>
    public void SwitchOff() {
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
