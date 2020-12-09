using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataSection : MonoBehaviour {
    public int enemyID = -1;                                    // To which enemy id the bar is linked at the moment.                  

    [Header("Status")]
    public bool displayed;                                      // Wheter the bar is displayed in the UI.

    [Header("Components")]
    public EnemyHPBar hpBar;                                    // Enemy HP bar class component reference.
    public EnemySprite enemySprite;                             // Enemy sprite class component reference.
    public TextComponent enemyName;                             // Enemy name class component reference.
    public GameObject sectionWrapper;                           // UI section wrapper. Use to hide - display enemy UI bar.

    [Header("Settings")]
    public float secondsDisplayed;                              // How long the enemy HP bar will be displayed in the screen after the battle has finished.

    private int _counter;                                       // Counter used to calculate how long the enemy UI data is displayed after battle.

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        if ( ! GameManager.instance.isPaused ) {
            /// check if the bar has to be hiden only when it is displayed.
            if ( displayed ) {
                CheckIfHideSection();
            }
        }
    }

    /// <summary>
    /// Display enemy UI section.
    /// </summary>
    public void Display() {
        sectionWrapper.SetActive( true );
        displayed = true;
    }

    /// <summary>
    /// Hide enemy UI section.
    /// </summary>
    public void Hide() {
        sectionWrapper.SetActive( false );
        displayed = false;
    }

    /// <summary>
    /// Check if hide section.
    /// </summary>
    private void CheckIfHideSection() {
        // 60 frames.
        if ( _counter <= ( secondsDisplayed * 60f ) ) {
            _counter++;
        } else {
            Hide();
            _counter = 0;
        }
    }

    /// <summary>
    /// Reset displayed enemy section
    /// internal counter.
    /// </summary>
    public void ResetBarDisplayedCounter() {
        _counter = 0;
    }

    /// <summary>
    /// Set up enemey HP section for new enemy.
    /// </summary>
    /// <param name="enemyID">int - to which enemy the bar will be linked to</param>
    /// <parma name="enemeyName">string - enemy name to display in the enemy UI section</param>
    /// <param name="enemySprite">Sprite - enemy 2D Sprite image.</param>
    /// <param name="currentHP">float - enemy current HP</param>
    /// <param name="maxHP">float - enemy max HP</param>
    public void SetUp( int enemyID, string enemyName, Sprite enemySprite, float currentHP, float maxHP ) {
        // set up new enemy ID.
        this.enemyID = enemyID;

        // set up visible sprite in the UI.
        this.enemyName.UpdateContent( enemyName );

        // set up enemy sprite.
        this.enemySprite.UpdateSprite( enemySprite );

        // set up enemy health bar.
        hpBar.SetUp( currentHP, maxHP );

        _counter = 0;
    }

}
