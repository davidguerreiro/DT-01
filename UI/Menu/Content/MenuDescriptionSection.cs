using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDescriptionSection : MonoBehaviour {
    public bool displayed;                                  // Displayed.
    [Header("Image Components")]
    public Image image;                                     // Image component reference.
    public FadeElement imageAnim;                           // Image fade element component reference.
    public Sprite defaultSprite;                            // Sprite to use if no sprite is provided for the image.

    [Header("Text components")]
    public FadeElement textAnim;                            // Text fade element component reference.
    public TextComponent text;                              // Text component element reference.

    [Header("Settings")]
    public float fadeSpeed = .5f;                           // Fade speed animation.
    
    private Coroutine _updateRoutine;                       // Update info coroutine.

    /// <summary>
    /// Update description
    /// components.
    /// </summary>
    /// <param name="content">string - text content</param>
    /// <param name="sprite">Sprite - sprite image to display</param>
    public void UpdateSection( string content, Sprite sprite = null ) {
        if ( _updateRoutine == null ) {
            _updateRoutine = StartCoroutine( content, sprite );
        }
    }

    /// <summary>
    /// Update description
    /// components coroutine.
    /// </summary>
    /// <param name="content">string - text content</param>
    /// <param name="sprite">Sprite - sprite image to display</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator UpdateSectionRoutine( string content, Sprite sprite = null ) {
        bool useImage = ( image != null && imageAnim != null );

        if ( displayed ) {
            displayed = false;
            
            if ( useImage && imageAnim.displayed ) {
                imageAnim.FadeOut();
            }
            textAnim.FadeOut();

            yield return new WaitForSecondsRealtime( fadeSpeed );
        }

        text.UpdateContent( content );
        textAnim.FadeIn();
        
        if ( useImage && sprite != null ) {
            image.sprite = sprite;
            imageAnim.FadeIn();
        }

        yield return new WaitForSecondsRealtime( .1f );
        displayed = true;
        _updateRoutine = null;
    }

}
