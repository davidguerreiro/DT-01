using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : Item {

    public HealthKitData healthKitData;
    
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
        Debug.Log( data.itemName_en + " used!" );
        
        useCoroutine = null;
        yield break;
    }
}
