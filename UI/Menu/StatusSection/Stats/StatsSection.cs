using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSection : MonoBehaviour {

    [Header("Data Source")]
    public PlayerStats data;                                            // Player data source scriptable.

    [Header("Components")]
    public MenuContentSection statsSection;                             // Current section content class reference.
    public TextComponent hp;                                            // HP text component reference.
    public TextComponent defense;                                       // Defense text component reference.
    public TextComponent skills;                                        // Skills text component reference.
    public TextComponent shards;                                        // Shards text component reference.

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if ( statsSection.displayed ) {
            UpdateScreenValues();
        }
    }

    /// <summary>
    /// Update values.
    /// </summary>
    private void UpdateScreenValues() {
        hp.UpdateContent( data.maxHitPoints.ToString() );
        defense.UpdateContent( data.defense.ToString() );
        skills.UpdateContent( data.currentSkills.ToString() );
        shards.UpdateContent( data.shards.ToString() );
    }
}
