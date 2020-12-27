using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSection : MonoBehaviour {
    public bool displayed;                              // Flag to control display status.

    [Header("Components")]
    public Image weaponImage;                           // Weapon image compontent reference.
    public TextComponent weaponName;                    // Weapon name compontent refernece.
    public TextComponent levelUpText;                   // Level up text component refernece.
    public LevelUpFeatures levelUpFeatures;             // Level up features class component reference.

    [Header("Settings")]
    public float secondsDisplayed;                      // Seconds displayed setting.
    public string reachedLevelTextEn;                   // Level text value for english language.
    public string reachedLevelTextEs;                   // Level text value for spanish language.

    private Animator _anim;
    private Coroutine _displayRoutine;                  // Display coroutine.
    private Coroutine _hideRoutine;                     // Hide coroutine.
    private float _displayedCounter;                    // Displayed internal counter.
    // private Vector3 _originalPosition;                  // Original hidden position.
    
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        if ( displayed ) {
            CheckIfHide();
        }
    }

    /// <summary>
    /// Check if box needs to be hidden.
    /// </summary>
    private void CheckIfHide() {
        // 1sec - 60 frames.
        if ( _displayedCounter < ( secondsDisplayed * 60f ) ) {
            _displayedCounter++;
        } else {
            HideBox();
            _displayedCounter = 0f;
        }
    }

    /// <summary>
    /// Display level up box
    /// in gameplay screeen.
    /// </summary>
    /// <param name="weaponData">PlasmaGun - wreapon scriptable object data</param>
    /// <param name="levelUpNots">string[] - level up notifications to be displayed.</param>
    public void DisplayBox( PlasmaGun weaponData, string[] levelUpNots ) {
        if ( ! displayed && _displayRoutine == null ) {
            _displayRoutine = StartCoroutine( DisplayBoxRoutine( weaponData, levelUpNots ) );
        }
    }

    /// <summary>
    /// Display level up box
    /// in gameplay screeen
    /// coutine.
    /// </summary>
    /// <param name="weaponData">PlasmaGun - wreapon scriptable object data</param>
    /// <param name="levelUpNots">string[] - level up notifications to be displayed.</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator DisplayBoxRoutine( PlasmaGun weaponData, string[] levelUpNots ) {
        weaponImage.sprite = weaponData.icon;
        weaponName.UpdateContent( weaponData.nameEn );      // TODO: Check for spanish.
        levelUpText.UpdateContent( reachedLevelTextEn + " " + weaponData.level.ToString() );

        _anim.SetBool( "Displayed", true );
        yield return new WaitForSeconds( 1f );

        // display notifications.
        levelUpFeatures.DisplayElements( levelUpNots );
        displayed = true;
        _displayRoutine = null;
    }

    /// <summary>
    /// Hide box.
    /// </summary>
    public void HideBox() {
        levelUpFeatures.HideElements();
        _anim.SetBool( "Displayed", true );
        displayed = false;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component.
        _anim = GetComponent<Animator>();

        // get original position.
        // _originalPosition = gameObject.transform.position;
    }
}
