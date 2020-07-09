using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : MonoBehaviour {
    public FPSInput player;                                     // Player controller class.
    public float idleSpeed = 0.2f;                              // Idle animation speed.
    public float idleWalkingSpeed = 0.7f;                       // Idle animtion walking speed.
    public float idleRunningSpeed = 2f;                         // Idle animation running speed.
    private Animator animator;                                  // Animator component reference.
    [HideInInspector]
    public enum AnimStates {                                    // Machine states for the animation.
        Stopped,
        Walking,
        Running,
    }

    [HideInInspector]
    public AnimStates animStates;                              // Instance for animation states.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        SetAnimation();
    }

    /// <summary>
    /// Set weapon animation.
    /// </summary>
    public void SetAnimation() {

        // detect player status to define animation.
        if ( player.grounded ) {

            if ( player.isMoving ) {
                animStates = AnimStates.Walking;

                if ( player.isRunning ) {
                    animStates = AnimStates.Running;
                }
            } else {
                animStates = AnimStates.Stopped;
            }
        } else {
            animStates = AnimStates.Stopped;
        }


        // trigger animation.
        switch( animStates ) {
            case AnimStates.Stopped:
                animator.SetFloat( "idleSpeed", idleSpeed );
                break;
            case AnimStates.Walking:
                animator.SetFloat( "idleSpeed", idleWalkingSpeed );
                break;
            case AnimStates.Running:
                animator.SetFloat( "idleSpeed", idleRunningSpeed );
                break;
            default:
                animator.SetFloat( "idleSpeed", idleSpeed );
                break;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component
        animator = GetComponent<Animator>();
    }
    
}
