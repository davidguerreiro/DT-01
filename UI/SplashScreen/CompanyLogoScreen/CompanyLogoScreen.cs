using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyLogoScreen : MonoBehaviour {

    [Header("Animation params")]
    public float secondsBeforeLogo = 1.5f;          // Seconds passed before the logo is displayed.
    public float secondsLogoDisplayed = 2f;         // Seconds passed after the logo is displayed.
    public float secondsAfterLogoDisplayed = 1.5f;  // Seconds passed between the logo disappears and the next scene is loaded.
    
    [Header("Next Scene")]
    public string nextScene;                        // Scene to load after the splash screen is done.

    // Start is called before the first frame update
    void Start() {
        StartCoroutine( DisplayCompanyLogo() );
    }

    /// <summary>
    /// Display company logo
    /// in the splash screen and
    /// then load next scene.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator DisplayCompanyLogo() {
        Animator animator = GetComponent<Animator>();

        if ( animator != null ) {

            yield return new WaitForSecondsRealtime( secondsBeforeLogo );
            animator.SetBool( "ToDisplay", true );

            yield return new WaitForSecondsRealtime( secondsLogoDisplayed );
            animator.SetBool( "ToHide", true );

            yield return new WaitForSecondsRealtime( secondsAfterLogoDisplayed );

            if ( Utils.instance != null ) {
                Utils.instance.LoadScene( nextScene );   
            }
        }
    }
}
