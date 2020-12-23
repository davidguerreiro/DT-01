using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTextFeature : MonoBehaviour {
    private TextComponent _text;                                  // Text component reference.
    private Animator _anim;                                       // Animator component refernece.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get text component reference.
        _text = GetComponent<TextComponent>();

        // get animator component reference.
        _anim = GetComponent<Animator>();
    }
}
