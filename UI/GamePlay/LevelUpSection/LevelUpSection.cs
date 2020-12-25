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

    private Animator _anim;
    private Coroutine _displayRoutine;                  // Display coroutine.
    private float _displayedCounter;                    // Displayed internal counter.
    
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Display level up box
    /// in gameplay screeen.
    /// </summary>
    /// <param name="weaponData">PlasmaGun - wreapon scriptable object data</param>
    // public IEnumerator 

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component.
        _anim = GetComponent<Animator>();
    }
}
