using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : Item {
    
    /// <summary>
    /// Use item action.
    /// </summary>
    public override void Use() {
        if ( useCoroutine == null ) {
            useCoroutine = StartCoroutine( UseRoutine() );
        }
    }

    /// <summary>
    /// Use item action coroutine.
    /// Item use logic goes here.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator UseRoutine() {
        useCoroutine = null;
        yield break;
    }
}
