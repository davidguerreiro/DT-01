using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;                        // Class static instance.
    public bool initDefaultSongAtStart;                         // Init default song when this level is initialisez.

    [Header("Components")]
    public LevelMusicController levelMusicController;           // Level music controller reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {
        if (initDefaultSongAtStart) {
            levelMusicController.PlaySong("common", "currentLevel");
        }
    }
}
