using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSectionUI : MonoBehaviour {
    public PlasmaGun data;                              // Main weapon data source.

    [Header("Experience Text")]
    public ExpText currentExp;                          // Current exp obtained exp text class component reference.
    public ExpText slashText;                           // Slash text exp text component reference.
    public ExpText totalExp;                            // Total exp required to level up class component reference.
    public ExpGot expGot;                               // Exp got from action class component refernece.

    [Header("Munition Circle")]
    public ExpCircleBar expCircleBar;                   // Experience bar class component reference.
    public MunitionCircle munitionCircle;               // Munition circle class component reference.
    public LevelSection levelSection;                   // Level section class component reference.

    [Header("Settings")]
    public float expGotSecondsDisplayed;                // How long the exp got message is displayed.
    public float expTextSecondsDisplayed;               // How long the exp message is displayed.

    private float _displayedCounter;                    // Displayed counter internal variable. Used to check when to hide displayed elements.

    // make munition and exp UI work within the game.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        if ( ! GameManager.instance.isPaused ) {
            
            // check to hide current exp displayed text elements.
            if ( currentExp.fadeElement.displayed || slashText.fadeElement.displayed || totalExp.fadeElement.displayed ) {
                CheckForHide();
            }
        }
    }

    /// <summary>
    /// Update UI when player gets
    /// exp.
    /// </summary>
    /// <param name="numericExpGot">int - experience got by the player</param>
    public void UpdateExpUI( int numericExpGot ) {
        
        // update text got component.
        expGot.Show( numericExpGot );

        // update text elements.
        UpdateExpGotComponent();

        // update exp circle bar.
        expCircleBar.UpdateExpBar();
    }

    /// <summary>
    /// Update current exp and total
    /// exp text components.
    /// </summary>
    private void UpdateExpGotComponent() {
        currentExp.text.UpdateContent( data.currentExp.ToString() );
        totalExp.text.UpdateContent( data.nextLevel.expRequired.ToString() );

        if ( ! currentExp.fadeElement.displayed ) {
            currentExp.fadeElement.FadeIn();
        }

        if ( ! totalExp.fadeElement.displayed ) {
            totalExp.fadeElement.FadeIn();
        }

        slashText.fadeElement.FadeIn();
        _displayedCounter = 0f;
    }

    /// <summary>
    /// Check counter to hide
    /// displayed elements.
    /// </summary>
    private void CheckForHide() {
        /// 60 frames = 1 sec in fixedUpdate.
        if ( _displayedCounter >= ( expTextSecondsDisplayed * 60f ) ) {
            currentExp.fadeElement.FadeOut();
            slashText.fadeElement.FadeOut();
            totalExp.fadeElement.FadeOut();
            _displayedCounter = 0f;
        } else {
            _displayedCounter++;
        }
    }
}
