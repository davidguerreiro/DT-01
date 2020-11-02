using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject prefab;                               // Prefab to be used in the object pool.
    public int poolSize;                                    // Size of the object pool.
    public List<GameObject> pool;                          // Object pool list.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start () {
        SetUpPool();
    }

    /// <summary>
    /// Set up object pool.
    /// </summary>
    private void SetUpPool() {

        pool = new List<GameObject>();

        for ( int i = 0; i < poolSize; i++ ) {
            GameObject prefabObject = Instantiate( prefab );
            prefabObject.transform.parent = transform;
            prefabObject.SetActive( false );

            pool.Add( prefabObject );
        }
    }

    /// <summary>
    /// Spawn prefab in the game scene.
    /// </summary>
    /// <param name="location">Vector3 - prefab location to be enabled</param>
    /// <returns>GameObject</returns>
    public GameObject SpawnPrefab( Vector3 location = new Vector3() ) {

        foreach ( GameObject prefab in pool ) {

            if ( prefab.activeSelf == false ) {

                prefab.SetActive( true );
                prefab.transform.localPosition = location;

                return prefab;
            }
        }

        return null;
    }
}
