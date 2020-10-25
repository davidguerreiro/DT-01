using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor {

    [Header("Player Actor Status")]
    public bool inAction = false;                                   // Flag to control action status.

    [HideInInspector]
    public Coroutine actionCoroutine;                               // Action coroutine refernece.

    /// <summary>
    /// Play action.
    /// </summary>
    /// <param name="actionName">string - action name.</param>
    /// <param name="actionType">Actor.ActionType - action type.</param>
    /// <param naem="duration">float - how many seconds the action will be played by the actor.</param>
    /// <param name="animSpeed">float - animation speed. Default to 1f.</param>
    /// <param name="backToIdle">bool - set idle as status after this action finishes playing.</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator PlayAction( string actionName, ActionType actionType, float duration, float animSpeed = 1f, bool backToIdle = false ) {
        if ( animated ) {
            inAction = true;

            if ( isMoving ) {
                StopMoving();
            }

            // set actor state.
            state = actionName;

            // perform action.
            switch ( actionType ) {
                case ActionType.bolean:
                    _anim.SetBool( actionName, true );
                    _anim.SetFloat( "AnimSpeed", animSpeed );
                    yield return new WaitForSeconds( duration );
                    _anim.SetBool( actionName, true ); 
                    break;
                case ActionType.trigger:
                    _anim.SetTrigger( actionName );
                    _anim.SetFloat( "AnimSpeed", animSpeed );
                    yield return new WaitForSeconds( duration );
                    break;
                default:
                    break;
            }

            // set anim speed to base value.
            _anim.SetFloat( "AnimSpeed", 1f );

            if ( backToIdle ) {
                state = "idle";
            }

            inAction = false;
            actionCoroutine = null;
        }
    }

    /// <summary>
    /// Stop current action.
    /// </summary>
    /// <param name="backToIdle">bool - if true, set idle state. False by default.</param>
    public void StopAction( bool backToIdle = false ) {
        if ( inAction ) {
            inAction = false;
        }

        if ( actionCoroutine != null ) {
            StopCoroutine( actionCoroutine );
            actionCoroutine = null;
        }

        if ( backToIdle ) {
            state = "idle";
        }
    }
}
