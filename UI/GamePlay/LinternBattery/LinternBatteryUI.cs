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

        if ( ! GameManager.instance.isPaused ) {

            // check whether the bar needs to be hiden.
            if ( _slider.value == _slider.maxValue ) {
                Hide();
            }
            
            // update slider value based on lintern data object value.
            _slider.value = linternData.currentBattery;

            // check if the bar has to be displayed.
            if ( ! displayed && linternData.enabled ) {
                Display();
            }

            // check whether the bar needs to become transparent.
            if ( displayed && ! _isTransparent && ! linternData.enabled && _slider.value < _slider.maxValue ) {
                UpdateToTransparent();
            }
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
        _isTransparent = false;
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
        _isTransparent = false;
        _toHalf = 0;
        _toHide = 0;
    }

    /// <summary>
    /// Set UI items transparent
    /// </summary>
    public void SemiHide() {
        icon.HalfFadeOut();
        barBackground.HalfFadeOut();
        barFill.HalfFadeOut();
        barText.HalfFadeOut();

        displayed = false;
        _isTransparent = true;
        _toHalf = 0;
    }


    /// <summary>
    /// Check wheter the bar has to become
    /// transparent.
    /// </summary>
    private void UpdateToTransparent() {

        // use 60 as a base reference for 1 sec = 60 frames.
        if ( _toHalf < timeToTransparent * 60 ) {
            _toHalf++;
        } else {
            SemiHide();
        }
    }

    /// <summary>
    /// Check when the bar has to be
    /// hiden.
    /// </summary>
    private void UpdateToHide() {

        // use 60 as a base reference for 1 sec = 60 frames.
        if ( _toHide < timeToTransparent * 60 ) {
            _toHide++;
        } else {
            Hide();
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
