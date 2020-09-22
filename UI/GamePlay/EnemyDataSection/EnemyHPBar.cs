using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour {

    [HideInInspector]
    public int enemyID = -1;                                // To which enemy id the bar is linked at the moment.
    public bool displayed;                                  // Wheter the bar is displayed in the UI.

    [Header("Components")]
    public EnemySprite enemySprite;                         // Enemy sprite class component reference.
    public FadeElement background;                          // Background fade class component reference.
    public FadeElement fill;                                // Fill fade class component reference.

    [Header("Settings")]
    public float offset;                                    // Extra value to ensure the bar is not too slow for enemies with low HP.
    public float toIgnoreOffset;                            // Ignore offset for enemy with HP superior to this amount.

    private Slider _slider;                                 // Slider component reference.
    private RectTransform _rect;                            // Rect transform component reference.


    // Start is called before the first frame update
    void Start() {
        Init();   
    }

    /// <summary>
    /// Update enemy HP value.
    /// </summary>
    /// <param name="currentHP">float - new enemy HP value.</param>
    public void UpdateHP( float currentHP ) {
        if ( _slider != null ) {
            _slider.value = currentHP;
        }
    }

    /// <summary>
    /// Display enemy HP bar in the gameplay
    /// UI.
    /// </summary>
    public void Display() {
        enemySprite.fadeClass.RawDisplay();
        background.RawDisplay();
        fill.RawDisplay();
        displayed = true;
    }

    /// <summary>
    /// Hide enemy HP bar in the gameplay
    /// UI.
    /// </summary>
    public void Hide() {
        enemySprite.fadeClass.FadeOut();
        background.FadeOut();
        fill.FadeOut();
        displayed = false;
    }

    /// <summary>
    /// Set up enemy HP bar for new enemy.
    /// </summary>
    /// <param name="enemyID">int - to which enemy the bar will be linked to</param>
    /// <param name="enemySprite">Sprite - enemy 2D Sprite image.</param>
    /// <param name="currentHP">float - enemy current HP</param>
    /// <param name="maxHP">float - enemy max HP</param>
    public void SetUp( int enemyID, Sprite enemySprite, float currentHP, float maxHP ) {

        if ( _slider != null && _rect != null ) {
            // set up new enemy ID
            this.enemyID = enemyID;

            // set up visible sprite in the UI
            this.enemySprite.UpdateSprite( enemySprite );

            // set up HP values.
            _slider.maxValue = maxHP;
            _slider.value = currentHP;

            // resize enemy HP bar to adjust to enemy max HP.
            float toAdd = ( maxHP < toIgnoreOffset ) ? offset : 0f;
            _rect.transform.localScale = new Vector3( toAdd, _rect.transform.localScale.y, _rect.transform.localScale.z );

            if ( ! displayed ) {
                Display();
            }
        }        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get slider component refernece.
        _slider = GetComponent<Slider>();

        // get rect transform component reference.
        _rect = GetComponent<RectTransform>();
    }
}
