using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyLoad : MonoBehaviour {

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        DontDestroyOnLoad( this.gameObject );    
    }
    
}
