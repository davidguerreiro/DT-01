using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinternBatteryUI : MonoBehaviour {

    [Header("Data Source")]
    public Lintern linternData;                             // Lintern data scriptable object reference.

    [Header("Status")]
    public bool displayed;                                  // Flag to control whether the lintern battery UI is displayed. 

    [Header("Visual Elements")]
    public FadeElement icon;                                // Linter icon.
    public FadeElement barBackground;                       // Battery bar background image.
    public FadeElement barFill;                             // Battery bar fill. 
    public FadeElement barText;                             // Battery text displayed below the battery bar.

    [Header("Settings")]
    public int timeToTransparent;                           // How long to wait until the UI becomes transparent when the battery is recharging.
    public int timeToHide;                                  // How long to wait unitl the UI is hidden after the battery has been fully charged.

    private Slider _slider;                                 // Slider UI component reference.
    private bool _isTransparent = false;                    // Flag to control whether the UI is displayed as transparent.
    private int _toHalf = 0;                                // Transparent UI internal counter.
    private int _toHide = 0;                                // Hide lintern UI internal counter.


    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    // Update is called once per frame.
    void Update() {
        
        // check if the battery bar has to be displayed.
        if ( ! displayed && linternData.enabled ) {
            Display();
        }

        // check if the battery bar has to start filling in.
        if ( displayed && ! linternData.enabled ) {
            FillIn();
        }

        // fill out battery bar if the lintern is in use.
        if ( linternData.enabled ) {
            FillOut();
        }
    }
    
    /// <summary>
    /// Set lintern battery current
    /// values when the UI is initialised.
    /// </summary>
    private void SetInitialValues() {

        if ( _slider != null ) {
            _slider.maxValue = linternData.maxBattery;
            _slider.value = linternData.currentBattery;
        }
    }

    /// <summary>
    /// Display UI elements.
    /// </summary>
    public void Display() {
        icon.FadeIn();
        barBackground.FadeIn();
        barFill.FadeIn();
        barText.FadeIn();

        _toHalf = 0;
        _toHide = 0;

        displayed = true;
    }

    /// <summary>
    /// Hide UI elements.
    /// </summary>
    public void Hide() {
        icon.FadeOut();
        barBackground.FadeOut();
        barFill.FadeOut();
        barText.FadeOut();
        displayed = false;
    }

    /// <summary>
    /// Set UI items transparent
    /// </summary>
    public void SemiHide() {
        icon.HalfFadeOut();
        barBackground.HalfFadeOut();
        barFill.HalfFadeOut();
        barText.HalfFadeOut();

        _toHalf = 0;
    }

    /// <summary>
    /// Fill out the bar.
    /// </summary>
    private void FillOut() {
        if ( _slider.value > 0f ) {
            
            _slider.value -= linternData.dischargeSpeed * Time.deltaTime;

            if ( _slider.value < 0f ) {
                _slider.value = 0f;
            }
        }
    }

    /// <summary>
    /// Fill in the bar.
    /// </summary>
    private void FillIn() {

        if ( _slider.value < linternData.maxBattery ) {
             
             _slider.value += linternData.chargeSpeed * Time.deltaTime;

             if ( _slider.value > linternData.maxBattery ) {
                 _slider.value = linternData.maxBattery;
             }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get slider component.
        _slider = GetComponent<Slider>();

        // set initial values for the slider UI component.
        SetInitialValues();
    }
}
