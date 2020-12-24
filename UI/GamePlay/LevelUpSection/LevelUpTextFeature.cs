using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTextFeature : MonoBehaviour {
    [HideInInspector]
    public TextComponent text;                                  // Text component reference.
    [HideInInspector]
    public FadeElement anim;                                       // Animator component refernece.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get text component reference.
        text = GetComponent<TextComponent>();

        // get animator component reference.
        anim = GetComponent<FadeElement>();
    }
}
