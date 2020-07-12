using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionSection : MonoBehaviour {
    public TextComponent buildVersionText;                              // Build version text component class reference.
    public TextComponent buildDateText;                                 // Build date text component.
    public BuildInfo buildInfo;                                         // Build info scriptable object instance.

    // Start is called before the first frame update
    void Start() {
        UpdateUIValues();
    }

    /// <summary>
    /// Update build values
    /// in the UI.
    /// </summary>
    private void UpdateUIValues() {
        buildVersionText.UpdateContent( buildInfo.version );
        buildDateText.UpdateContent( buildInfo.lastBuildDate );
    }
}
