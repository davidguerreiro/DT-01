using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour {
    public string currentSelected;                                              // Current selected.
    public MenuNavigationButton[] navigationButtons;                            // Menu navigation buttons reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Disable all other buttons
    /// when a new section is selected.
    /// </summary>
    /// <param name="current">string - new current selected. Used to avoid disabling the new section.</param>
    public void DisableAll( string current ) {
        foreach ( MenuNavigationButton navigationButton in navigationButtons ) {
            if ( navigationButton.sectionName != currentSelected ) {
                navigationButton.UnSelected();
            }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        // set current selected ( by default the first section in the menu buttons array ).
        if ( navigationButtons.Length > 0 ) {
            currentSelected = navigationButtons[0].sectionName;
        }
    }
    
}
