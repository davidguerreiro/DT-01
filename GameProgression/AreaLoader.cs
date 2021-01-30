using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour {
    
    public GameObject[] objectsToLoad;                          // Objects to load in the scene.
    public GameObject[] objectsToUnload;                        // Objects to unload in the scene.

    /// <summary>
    /// Load and unload objects.
    /// </summary>
    private void LoadUnloadObjects() {
        foreach ( GameObject element in objectsToLoad ) {
            element.SetActive(true);
        }
        
        foreach( GameObject element in objectsToLoad ) {
            element.SetActive(false);
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            LoadUnloadObjects();
        }
    }
}
