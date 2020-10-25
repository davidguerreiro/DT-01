using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardsSectionUI : MonoBehaviour {

    [Header("Status")]
    public bool displayed;                  // Flag to control whether the shards UI is displayed or not.

    [Header("Data Source")]
    public PlayerStats playerData;          // Player data - source data object reference.

    [Header("Components")]
    public FadeElement iconFade;            // Icon fade class component reference.
    public FadeElement textFade;            // Text fade class component reference.
    public TextComponent textValue;         // Text component class reference.

    [Header("Settings")]
    public float secondsVisible;            // How long the elements are displayed in the UI after the player collects shards.
    public float fadeOutSpeed;              // Fade out elements speed.

    private int _displayedCounter;           // Counter to check when the UI elements have to be hidden.

    // Update is called once per frame
    void Update() {

        if ( ! GameManager.instance.isPaused ) {
        
            if ( displayed ) {

                // update shards amount in the screen.
                UpdateShardsAmount();

                // update hide UI elements counter.
                UpdateHideCounter();
            }
        }
    }

    /// <summary>
    /// Update shards visible amount
    /// in the UI.
    /// </summary>
    private void UpdateShardsAmount() {
        textValue.UpdateContent( playerData.shards.ToString() );
    }

    /// <summary>
    /// Display UI elements.
    /// </summary>
    public void Display() {
        iconFade.FadeIn();
        textFade.FadeIn();
        displayed = true;

        _displayedCounter = 0;
    }

    /// <summary>
    /// Hide UI elements.
    /// </summary>
    public void Hide() {
        iconFade.FadeOut( fadeOutSpeed );
        textFade.FadeOut( fadeOutSpeed );
        displayed = false;

        _displayedCounter = 0;
    }

    /// <summary>
    /// Update displayed counter
    /// to calculate when the elements
    /// have to hide.
    /// </summary>
    private void UpdateHideCounter() {

        if ( _displayedCounter < ( secondsVisible * 60f ) ) {
            _displayedCounter++;
        } else {
            Hide();
        }
    }

}
