using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {
    public string mainMenuScene;                        // Main menu scene to load after the splashscreen is completed.

    [Header("Elements")]
    public FadeElement textLogo;                         // Text logo fade element.
    public FadeElement[] otherText;                      // Other text fade element.

    [Header("Settings")]
    public float secondsBeforeLogo = 1f;                 // Seconds to wait till the logo text is displayed.
    public float secondsLogoDisplayed = 1f;              // Seconds to wait till the logo is removed from the screen.
    public float secondsBeforeText = 1f;                 // Seconds before displaying all the remaining text elements.
    public float secondTextDisplayed = 1f;               // Seconds to wait till the text is removed from the screen.
    public float secondsBeforeLoadingMenu = 1f;          // Seconds before loading main menu scene.

    private Coroutine splashCoroutine;                   // Splash Screen coroutine.

    // Start is called before the first frame update
    void Start() {
        splashCoroutine = StartCoroutine( SplashScreenCoroutine() );
    }

    /// <summary>
    /// SplashScreen coroutine.
    /// Display logo text in the screen,
    /// then display user info text and after
    /// that load main menu scene.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SplashScreenCoroutine() {
        yield return new WaitForSecondsRealtime( secondsBeforeLogo );
        textLogo.FadeIn();

        yield return new WaitForSecondsRealtime( secondsLogoDisplayed );
        textLogo.FadeOut();

        yield return new WaitForSecondsRealtime( secondsBeforeText );
        foreach ( FadeElement textElement in otherText ) {
            textElement.FadeIn();
        }

        yield return new WaitForSecondsRealtime( secondTextDisplayed );
        foreach ( FadeElement textElement in otherText ) {
            textElement.FadeOut();
        }

        yield return new WaitForSecondsRealtime( secondsBeforeLoadingMenu );
        SceneManager.LoadScene( mainMenuScene );
    }
}
