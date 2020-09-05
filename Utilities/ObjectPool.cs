using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject prefab;                               // Prefab to be used in the object pool.
    public int poolSize;                                    // Size of the object pool.
    private static List<GameObject> pool;                   // Object pool list.

    /// <summary>
    /// Set up object pool.
    /// </summary>
    private void SetUpPool() {

        if ( pool == null ) {
            pool =  new List<GameObject>();
        }

        for ( int i = 0; i < poolSize; i++ ) {
            GameObject prefabObject = Instantiate( prefab );
            prefabObject.SetActive( false );

            pool.Add( prefabObject );
        }
    }
}
