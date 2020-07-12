using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenuItem : MonoBehaviour {
    private Animator _animator;                                     // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Hover item.
    /// </summary>
    public void Hover() {
        _animator.SetBool( "Hover", true );
    }

    /// <summary>
    /// Unhover item.
    /// </summary>
    public void UnHover() {
        _animator.SetBool( "Hover", false );
    }

    /// <summary>
    /// Load scene when the
    /// item is clicked.
    /// </summary>
    public void LoadScene() {

        SceneLoader sceneLoader = GetComponent<SceneLoader>();

        if ( sceneLoader != null ) {
            sceneLoader.LoadScene();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator componentr.
        _animator = GetComponent<Animator>();
    }
}
