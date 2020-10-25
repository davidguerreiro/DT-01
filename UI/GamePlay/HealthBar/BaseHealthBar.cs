using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour {
    [Header("Required Components")]
    public PlayerStats playerData;                                  // Player stats scriptable object reference.
    public GameObject hpDisplayed;                                  // Current player HP value displayed in-game.

    [Header("HP Value Settings")]
    public bool toHideHP = true;                                    // Whether the hp text has to be hidden or not.
    public float hpSecondsVisible = 5f;                             // Seconds the hp text is visible in the screen.
    public float fadeOutSpeed = 0.3f;                               // HP text fade out animation.
    private Slider _slider;                                         // UI slider component reference.
    private TextComponent _hpText;                                  // HP displayed text component reference.
    private FadeElement _hpTextFade;                                // HP displayed text fade element component reference.
    private float _hideCounter = 0f;                                // Hide text value counter.                             
    private float _prevValue = 0f;                                  // Previous health value. Used to check wheter we add or subtract hp in the bar.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {

        if ( ! GameManager.instance.isPaused ) {

            // check if text hp value has to be hidden.
            if ( toHideHP && _hpTextFade.displayed ) {
                HideHPValueChecker();
            }

            // update health bar with real player hp data.
            UpdateSliderFromPlayerData();
        }
    }

    /// <summary>
    /// Update hp bar slider
    /// value and text value from player
    /// stats real hp data.
    /// </summary>
    /// <returns>void</returns>
    private void UpdateSliderFromPlayerData() {

        // update slider value.
        _prevValue = _slider.value;
        _slider.value   = playerData.hitPoints;

        // update text value from health bar data.
        _hpText.UpdateContent( _slider.value.ToString() );  
    }

    /// <summary>
    /// Check if the hp has to be hide.
    /// </summary>
    private void HideHPValueChecker() {
        
        // use 60 for frame standard count.
        if ( _hideCounter < ( hpSecondsVisible * 60f ) ) {
            _hideCounter++;
        } else {

            // trigger hide hp animation.
            _hpTextFade.FadeOut( fadeOutSpeed );

            _hideCounter = 0f;
        }
    }

    /// <summary>
    /// Display health value when player's
    /// hit ponits are updated.
    /// </summary>
    /// <returns>void</returns>
    public void DisplayHPValue() {

        if ( _hpTextFade == null ) {
            _hpTextFade = hpDisplayed.GetComponent<FadeElement>();
        }
        
        _hpTextFade.FadeIn();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get slider component reference.
        _slider = GetComponent<Slider>();

        if ( hpDisplayed != null ) {

            // get text component from hp text gameObject.
            _hpText = hpDisplayed.GetComponent<TextComponent>();

            // get fade component from hp text gameObject.
            _hpTextFade = hpDisplayed.GetComponent<FadeElement>();

        }
    }
}
