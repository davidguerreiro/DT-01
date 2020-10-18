using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager instance;                 // Public static class instance reference.

    [Header("Components")]
    public FadeElement title;                               // Title fade element component.
    public FadeElement logo;                                // Logo fade element component.
    public FadeElement[] menuItems;                         // Menu items element component.
    public FadeElement versionText;                         // Version text fade element component.
    public FadeElement screenCover;                         // Screen cover fade element component.
    public Animator cameraAnim;                             // Camera animator component reference.

    [Header("Settings")]
    public float secondsBeforeRemoveCover = 1f;             // Seconds to wait before removing scene cover.
    
    private AudioComponent _audio;                          // Audio component reference.
    private bool _inCinematic = false;                      // In cinematic control flag.
    private Coroutine _mainMenuCinematicCoroutine;          // Main menu scene coroutine.
    
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

        // start cinematic.
        _mainMenuCinematicCoroutine = StartCoroutine( MainMenuCinematic() );
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Main menu cinematic coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator MainMenuCinematic() {
        _inCinematic = true;
        yield return new WaitForSecondsRealtime( 1f );

        if ( _audio != null ) {
            _audio.PlaySound();
        }

        yield return new WaitForSecondsRealtime( secondsBeforeRemoveCover );
        screenCover.FadeOut( 0.3f );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
