using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpText : MonoBehaviour {

    [HideInInspector]
    public TextComponent text;                          // Text component reference.

    [HideInInspector]
    public FadeElement fadeElement;                  // Fade element component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class methdo.
    /// </summary>
    private void Init() {

        // get text component reference.
        text = GetComponent<TextComponent>();

        // get fade component reference.
        fadeElement = GetComponent<FadeElement>();
    }
}
