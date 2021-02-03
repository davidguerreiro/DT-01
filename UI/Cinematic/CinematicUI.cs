using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicUI : MonoBehaviour {
    public static CinematicUI instance;                             // Static class instance.
    
    [Header("Components")]
    public FadeElement cover;                                       // Cover.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        if ( instance == null ) {
            instance = this;
        }
    }
}
