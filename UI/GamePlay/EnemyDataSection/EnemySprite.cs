using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySprite : MonoBehaviour {

    [HideInInspector]
    public FadeElement fadeClass;                         // Fade element class component reference.
    private Image _image;                                   // Image component reference.
    

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Update enemy sprite.
    /// </summary>
    /// <param name="enemySprite">Sprite - Enemy sprite to display in the gameplay UI</param>
    public void UpdateSprite( Sprite enemySprite ) {
        if ( _image != null ) {
            _image.sprite = enemySprite;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get image component refernece.
        _image = GetComponent<Image>();

        // get fade element class component reference.
        fadeClass = GetComponent<FadeElement>();

    }
}
