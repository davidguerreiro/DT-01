using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public string sceneToLoad;                              // Scene to load by this script.

    /// <summary>
    /// Load scene by name.
    /// </summary>
    public void LoadScene() {
        SceneManager.LoadScene( sceneToLoad );
    }

    /// <summary>
    /// Load scene by name.
    /// </summary>
    public void LoadScene( string sceneName ) {
        SceneManager.LoadScene( sceneName );
    }
}
