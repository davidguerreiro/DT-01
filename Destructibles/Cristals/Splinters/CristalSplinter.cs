using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalSplinter : MonoBehaviour {

    private Animator _animator;                                     // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Fade out splinter from
    /// scene.
    /// </summary>
    private void FadeOutSplinter() {
        _animator.SetBool( "FadeOut", true );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _animator = GetComponent<Animator>();
    }
}
