using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpCircleBar : MonoBehaviour {

    public PlasmaGun data;                               // Werapon data source.

    [HideInInspector]
    public float nextLevelExp;                           // Next level exp neccesary to upgrade weapon to next level.

    private Image _bar;                                  // Filled image bar component reference.
    private Animator _anim;                              // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update exp
    /// bar.
    /// </summary>
    /// <param name="currentExp">int - weapon current experience.</param>
    public void UpdateExpBar( int currentExp ) {
        float fCurrentExp = (float) currentExp;

        _anim.SetTrigger( "Flash" );
        
        float normalized = Utils.instance.Normalize( fCurrentExp, 0f, nextLevelExp );
        _bar.fillAmount = normalized;
    }

    /// <summary>
    /// Set up exp level
    /// bar. This method must be called
    /// when the scene is intialised or
    /// player switchs weapon.
    /// </summary>
    /// <param name="currentExp">int - weapon current experience.</param>
    /// <param name="nextLevelExp">int - weapon next level exp required</param>
    public void SetUp( int currentExp, int nextLevelExp ) {
        float fCurrentExp = (float) currentExp;
        this.nextLevelExp = (float) nextLevelExp;

        float normalized = Utils.instance.Normalize( fCurrentExp, 0f, this.nextLevelExp );
        _bar.fillAmount = normalized;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get image component.
        _bar = GetComponent<Image>();

        // get animator component.
        _anim = GetComponent<Animator>();

        // set up exp bar with current weapon exp -- max level for demo version is 10.
        int nextLevel = ( data.level < 10 ) ? data.level + 1 : 10;
        PlasmaGunLevel nextLevelData = data.GetLevelDataObject( nextLevel );
        float normalizedValue = Utils.instance.Normalize( data.currentExp, 0, nextLevelData.expRequired );
        _bar.fillAmount = normalizedValue;
    }
}
