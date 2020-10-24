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
    public FadeElement dsTopText;                           // Developer section Top Text element component.
    public FadeElement dsText;                              // Developer section text element component.
    public FadeElement screenCover;                         // Screen cover fade element component.
    public Animator cameraAnim;                             // Camera animator component reference.
    public MainTitleCinematic mainTitleCinematic;           // Main title cinematic component reference.

    [Header("Cinematic Settings")]
    public float secondsBeforeRemoveCover = 1f;             // Seconds to wait before removing scene cover.
    public float secondsBeforeDisplayDSText = 1f;           // Seconds before display developer text in the game scene.
    public float secondsDSTextDisplayed = 1f;               // Seconds the Developer text is displayed in the game scene.
    public float secondsBeforeDisplayingTitle = 1f;         // Seconds before displaying game title in the game scene.
    public float secondsBeforeDisplayingComponents = 1f;    // Seconds before displaying the rest of the components in the game scene.

    [Header("Animations Settings")]
    public float screenCoverFadeInSpeed = 1f;               // Screen cover fade in animation speed.
    public float screenCoverFadeOutSpeed = 1f;              // Screen cover fade out animation speed.
    public float developerTextAnimSpeed = 1f;               // Developer text fade animation speed.
    public float titleAnimationSpeed = 1f;                  // Title animation speed.
    public float startGameAnimationWait = 1f;               // Start game animation wait time between screen cover and game

    
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

        // play camera cinematic animation and play player actor movement animation.
        cameraAnim.SetBool( "PlayCinematic", true );
        yield return new WaitForSecondsRealtime( .5f );
        mainTitleCinematic.Play();
        screenCover.FadeOut( screenCoverFadeOutSpeed );

        // display developer text.
        yield return new WaitForSecondsRealtime( secondsBeforeDisplayDSText );
        dsTopText.FadeIn( developerTextAnimSpeed );
        dsText.FadeIn( developerTextAnimSpeed );

        // hide developer text.
        yield return new WaitForSecondsRealtime( secondsDSTextDisplayed );
        dsTopText.FadeOut( developerTextAnimSpeed );
        dsText.FadeOut( developerTextAnimSpeed );

        // display game title.
        yield return new WaitForSecondsRealtime( secondsBeforeDisplayingTitle );
        title.FadeIn( titleAnimationSpeed );

        // display rest of game components.
        yield return new WaitForSecondsRealtime( secondsBeforeDisplayingComponents );
        logo.FadeIn();
        yield return new WaitForSecondsRealtime( .8f );
        versionText.FadeIn();
        yield return new WaitForSecondsRealtime( .8f );

        // display menu.
        foreach ( FadeElement menuItem in menuItems ) {
            menuItem.FadeIn( .2f );
        }

        yield return new WaitForSecondsRealtime( 1f );

        for ( int i = 0; i < menuItems.Length; i++ ) {
            if ( i > 0 ) {
                menuItems[i].HalfFadeOut( .7f );
            }
        }

        yield return new WaitForSecondsRealtime( 1f );

        if ( UIManager.instance != null ) {
            UIManager.instance.cursorEnabled = true;
        }

        _inCinematic = false;
    }

    /// <summary>
    /// Start game UI animation.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator StartGameAnim() {
        StartCoroutine( _audio.FadeOutSongRoutine() );
        screenCover.FadeIn( .6f );

        yield return new WaitForSecondsRealtime( startGameAnimationWait );
        title.FadeOut( .1f );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
