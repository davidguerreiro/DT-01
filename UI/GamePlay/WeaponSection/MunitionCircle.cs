using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MunitionCircle : MonoBehaviour {

    public PlasmaGun data;                               // Data source.

    [Header("Components")]
    public  Image munitionBar;                                  // Filled image bar component reference.
    public Animator munitionBarAnim;                            // Animator component reference.
    public TextComponent munitionQuantity;                      // Munition quantity text.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if ( ! GameManager.instance.isPaused ) {

            // check for heated status.
            ListenForHeated();

            // updat munition quantity.
            UpdateMunitionQuantityText();
            
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        if ( ! GameManager.instance.isPaused ) {
            UpdateCircle();
        }
    }

    /// <summary>
    /// Update munition circle.
    /// </summary>
    private void UpdateCircle() {
        float normalizeValue = Utils.instance.Normalize( data.plasma, 0, data.maxPlasma );
        munitionBar.fillAmount = normalizeValue;
    }

    /// <summary>
    /// Update munition quantity
    /// text.
    /// </summary>
    private void UpdateMunitionQuantityText() {
        munitionQuantity.UpdateContent( data.plasma.ToString() );
    }

    /// <summary>
    /// Listen for heated.
    /// </summary>
    public void ListenForHeated() {
        if ( data.IsBelowThreshold() ) {
            munitionBarAnim.SetBool( "Heated", true );
        } else {
            munitionBarAnim.SetBool( "Heated", false );
        }
    }

    

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        // set up current muntion value.
        munitionQuantity.UpdateContent( data.plasma.ToString() );
    }
}
