using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour {
    public PlasmaGun data;                                  // Main weapon data.

    [Header("Components")]
    public TextComponent levelText;                         // Level text text component class reference.
    public Animator levelTextAnim;                          // Level text animatior component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summaru>
    /// Update UI when the player levels
    /// up current weapon.
    /// </summary>
    public void UpdateUI() {
        levelText.UpdateContent( data.level.ToString() );
        levelTextAnim.SetTrigger( "LevelUp" );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // set up current level.
        levelText.UpdateContent( data.level.ToString() );
    }
}
