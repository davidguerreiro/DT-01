using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpCircleBar : MonoBehaviour {

    [HideInInspector]
    public float nextLevelExp;                             // Next level exp neccesary to upgrade weapon to next level.

    private Image _bar;                                  // Filled image bar component reference.
    private Animator _anim;                              // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set up exp level
    /// bar.
    /// </summary>
    public void SetUp( int currentExp, int nextLevelExp ) {
        float fCurrentExp = (float) currentExp;
        this.nextLevelExp = (float) nextLevelExp;

        float normalized = Utils.instance.Normalize( fCurrentExp, 0f, this.nextLevelExp );
//         _bar.
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get image component.
        _bar = GetComponent<Image>();

        // get animator component.
        _anim = GetComponent<Animator>();
    }
}
