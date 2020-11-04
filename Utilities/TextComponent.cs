using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : MonoBehaviour {
    private Text _content;                                          // Text component reference.

    // Start is called before the first frame update
    void Start() { 
        Init();
    }

    /// <summary>
    /// Get current text
    /// content.
    /// </summary>
    /// <returns>string</returns>
    public string GetContent() {
        return _content.text;
    }

    /// <summary>
    /// Update content.
    /// </summary>
    /// <param name="newContent">string - new content to be displayed in this text component.</param>
    public void UpdateContent( string newContent ) {

        if ( _content == null ) {
            _content = GetComponent<Text>();
        }

        _content.text = newContent;
    }

    /// <summary>
    /// Update text colour.
    /// </summary>
    /// <param name="colour">color - Colour to apply to the text</param>
    public void UpdateColour( Color colour ) {
        _content.color = colour;
    }

    /// <summary>
    /// Get current text colour.
    /// </summary>
    public Color GetColour() {
        return _content.color;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {

        // get text component reference.
        if ( _content == null ) {
            _content = GetComponent<Text>();
        }
    }
}
