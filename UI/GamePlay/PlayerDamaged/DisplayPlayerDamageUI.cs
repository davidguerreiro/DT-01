using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerDamageUI : MonoBehaviour {
    private Animator _animator;                         // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Display player UI damage
    /// element on the gameplay UI.
    /// </summary>
    public void Display() {
        _animator.SetTrigger( "display" );
    }
    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _animator = GetComponent<Animator>();
    }
}
