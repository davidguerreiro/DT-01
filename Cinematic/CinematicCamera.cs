using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour {
    [HideInInspector]
    public Camera camera;                                   // Camera component reference.

    [Header("Events")]
    public bool[] events = new bool[7];                     // Events array - used to trigger events from animators and other sources.
    private Animator animator;                              // Animator component reference.


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Set animation speed
    /// parameter.
    /// </summary>
    /// <param name="speed">float - camera animation speed</param>
    public void SetAnimSpeed( float speed ) {
        animator.SetFloat( "speed", speed );
    }


    /// <summary>
    /// Trigger float animation.
    /// </summary>
    /// <param name="parameter">string - parameter name</param>
    /// <param name="value">float - parameter value</param>
    public void PlayFloatAnim( string parameter, float value ) {
        animator.SetFloat( parameter, value );
    }

    /// <summary>
    /// Trigger int animation.
    /// </summary>
    /// <param name="parameter">string - parameter name</param>
    /// <param name="value">int - parameter value</param>
    public void PlayIntAnim( string parameter, int value ) {
        animator.SetInteger( parameter, value );
    }

    /// <summary>
    /// Trigger bool animation.
    /// </summary>
    /// <param name="parameter"string - parameter name</param>
    /// <param name="value">bool - parameter value</param>
    public void PlayBoolAnim( string parameter, bool value ) {
        animator.SetBool( parameter, value );
    }

    /// <summary>
    /// Trigger 'trigger' animation.
    /// </summary>
    /// <param name="parameter"string - parameter name</param>
    public void PlayBoolAnim( string parameter ) {
        animator.SetTrigger( parameter );
    }

    /// <summary>
    /// Set event in events array.
    /// </summary>
    public void SetEvent() {
        for (int i = 0; i < events.Length; i++) {
            if ( !events[i] ) {
                events[i] = true;
                break;
            }
        }
    }

    /// <summary>
    /// Set event in events array.
    /// </summary>
    /// <parma name="key">int - array key whose event must be set.</param>
    public void SetEventByKey(int key) {
        events[key] = true;
    }

    /// <summary>
    /// Restore events.
    /// </summary>
    public void RestoreEvents() {
        for (int i = 0; i < events.Length; i++) {
            events[i] = false;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        camera = GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }
}
