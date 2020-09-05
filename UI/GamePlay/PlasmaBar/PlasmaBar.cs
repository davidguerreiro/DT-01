using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlasmaBar : MonoBehaviour {

    [Header("Data Source")]
    public PlasmaGun plasmaGunData;                                     // Plasma gun scriptable object reference.


    [Header("Animation")]
    public bool displayed;                                              // Wheter the plasma bar is displayed by default. Should be false at initiation level.
    public float secondsVisible;                                        // For how long it should be visible after updating.
    public float fadeOutSpeed;                                          // Fade out animation speed.
    public FadeElement backgroundFade;                                  // Background Fade element class reference.
    public FadeElement fillFade;                                        // Fill Fade element class reference.

    [Header("Text Displayed")]
    public GameObject plasmaText;                                       // Plasma text value gameObject.
    public Animator heatedWarningText;                                  // Heated warning text animator.
    private Slider _slider;                                             // Slider component reference.
    private TextComponent _plasmaTextComponent;                         // Plasma text value displayed component.
    private FadeElement _plasmaTextFade;                                // Plasma text Fade component class reference.
    private float _hideCounter = 0;                                     // Counter used to hide the plasma bar once it is fully filled.
    private float _rechargeCounter = 0;                                 // Recharge counter used to calculate recharge bar speed.
    private float _heatedThreshold;                                      // Heated threshold used to calculate recharge speed.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
        // Update slider value.
        UpdateSliderValue();

        // check if the slider bar needs recharging.
        if ( displayed && _slider.value < _slider.maxValue ) {
            RechargePlasmaBar();
        }

        // check if the hide elements counter needs to run.
        if ( displayed && _slider.value == _slider.maxValue ) {
            CheckIfFadeOut();
        }

        // check for heated status.
        if ( displayed ) {
            CheckIfHeated();
        }
    }

    /// <summary>
    /// Update slider value based in
    /// plasma gun.
    /// </summary>
    private void UpdateSliderValue() {
        if ( _slider != null && plasmaGunData != null ) {
            _slider.value = plasmaGunData.plasma;
            _plasmaTextComponent.UpdateContent( _slider.value.ToString() );
        }
    }

    /// <summary>
    /// Check if the pasma gun is
    /// heated.
    /// </summary>
    private void CheckIfHeated() {

        if ( plasmaGunData.heated ) {
            heatedWarningText.SetBool( "displayWarning", true );
        } else {
            heatedWarningText.SetBool( "displayWarning", false );
        }
    }

    /// <summary>
    /// Recharge bar.
    /// </summary>
    private void RechargePlasmaBar() {
        
        // calculated recharge speed based on weapon heated status.
        float toWait = ( plasmaGunData.heated ) ? plasmaGunData.heatedRechargeSpeed :  plasmaGunData.rechargeSpeed; 

        // used 60 as a base frame standard.
        if ( _rechargeCounter < ( toWait * 60f ) ) {
            _rechargeCounter++;
        } else {
            _rechargeCounter = 0;
            plasmaGunData.plasma++;
        }
        
    }

    /// <summary>
    /// Check if bar needs to disapear.
    /// </summary>
    private void CheckIfFadeOut() {

        // use 60f as a base frame standard.
        if ( _hideCounter < ( secondsVisible * 60f ) ) {
            _hideCounter++;
        } else {
            _hideCounter = 0;
            FadeOutBar();
        }
    }

    /// <summary>
    /// Fade out plasma bar.
    /// </summary>
    private void FadeOutBar() {
        
        if ( backgroundFade != null && fillFade != null && _plasmaTextFade != null ) {
            backgroundFade.FadeOut( fadeOutSpeed );
            fillFade.FadeOut( fadeOutSpeed );
            _plasmaTextFade.FadeOut( fadeOutSpeed );
            displayed = false;
        }

    }

    /// <summary>
    /// Fade in plasma bar.
    /// </summary>
    private void FadeInBar() {

        if ( backgroundFade != null && fillFade != null && _plasmaTextFade != null ) {
            backgroundFade.FadeIn();
            fillFade.FadeIn();
            _plasmaTextFade.FadeIn();

            // reset hide counter.
            displayed = true;
        }

    }

    /// <summary>
    /// Logic to be triggered every
    /// time the slider changes value.
    /// </summary>
    public void SliderValueOnChange() {

        if ( _slider != null && ! displayed && ( _slider.value < _slider.maxValue ) ) {
            FadeInBar();
        }

        _hideCounter = 0f;

    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get slider component reference.
        _slider = GetComponent<Slider>();

        // set plasma slider max value and value.
        if ( plasmaGunData != null ) {
            _slider.value = plasmaGunData.plasma;
            _slider.maxValue = plasmaGunData.maxPlasma;
        }

        // get plasma text value displayed components
        if ( plasmaText != null ) {
            _plasmaTextComponent = plasmaText.GetComponent<TextComponent>();
            _plasmaTextFade = plasmaText.GetComponent<FadeElement>();
        }

        // calculate headed threshold.
        if ( _slider != null ) {
            _heatedThreshold = ( plasmaGunData.heatedRechargeThreeshold / _slider.maxValue ) * 100f;
        }
    }

}
