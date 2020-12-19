using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpCircleBar : MonoBehaviour {

    public PlasmaGun data;                               // Werapon data source.


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
    /// <param name="displayAnim">bool - display flash animation. True by default</param>
    public void UpdateExpBar( bool displayAnim = true ) {
        int nextLevel = ( data.level < 10 ) ? data.level + 1 : 10;
        PlasmaGunLevel nextLevelData = data.GetLevelDataObject( nextLevel );
        float normalizedValue = Utils.instance.Normalize( data.currentExp, 0, nextLevelData.expRequired );
        _bar.fillAmount = normalizedValue;
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
        UpdateExpBar( false );
    }
}
