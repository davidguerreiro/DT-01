using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuContentSection : MonoBehaviour {
    public string sectionName;                                  // Section name. This should be unique.
    public bool displayed;                                      // Flag to control if the section is displayed.
    public FadeElement cover;                                   // Section cover.

    /// <sumamry>
    /// Display section.
    /// </summary>
    public void Display() {
        if ( ! displayed ) {
            cover.FadeOut();
            displayed = true;
        }
    }

    /// <summary>
    /// Hide section.
    /// </summary>
    public void Hide() {
        if ( displayed ) {
            cover.FadeIn();
            displayed = false;
        }
    }
}
