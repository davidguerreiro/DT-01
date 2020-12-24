using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpFeatures : MonoBehaviour {
    public LevelUpTextFeature[] textFeatures;                           // Text features reference to display text about improved attributes when leveling up.

    [Header("Settings")]
    public float secondsInBetween;                                      // Seconds between each feature is displayed.

    private Coroutine _displayRoutine;                                  // Display text features coroutine.

    /// <summry>
    /// Display text features.
    /// </summary>
    /// <param name="textData">Array - text data array to display</param>
    public void DisplayElements( string[] textData ) {
        if ( _displayRoutine != null ) {
            _displayRoutine = StartCoroutine( DisplayElementsRoutine( textData ) );
        }
    }

    /// <summry>
    /// Display text features coroutine.
    /// </summary>
    /// <param name="textData">Array - text data array to display</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator DisplayElementsRoutine( string[] textData ) {
        for( int i = 0; i < textData.Length; i++ ) {
            textFeatures[i].text.UpdateContent( textData[i] );
            textFeatures[i].anim.FadeIn();

            yield return new WaitForSeconds( secondsInBetween );
        }
        _displayRoutine = null;
    }

    /// <summary>
    /// Hide all text features.
    /// </summary>
    public void HideElements() {
        foreach ( LevelUpTextFeature textFeature in textFeatures ) {
            textFeature.text.UpdateContent("");
            if ( textFeature.anim.displayed ) {
                textFeature.anim.FadeOut();
            }
        }
    }
}
