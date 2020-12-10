using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {

    [Header("Settings")]
    public Vector3 initPosition;                            // Damage text instance init position.
    public Color defaultColor;                              // Default text color.
    public Color criticalColor;                             // Damage text color used when an impact is critic.

    private TextComponent _text;                            // Text component 
    private Animator _anim;                                 // Animator component reference.

    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    /// <summary>
    /// Display text component.
    /// </summary>
    /// <param name="enemyDamage">float - damage value to display.</param>
    /// <parma bool="isCritic">bool - whether the impact has been critic.False by default</param>
    public void Display( float enemyDamage, bool isCritic = false ) {
        transform.localPosition = initPosition;
        
        string damageToDisplay = enemyDamage.ToString("F2").Replace(",", "." );
        _text.UpdateContent( damageToDisplay );

        if ( ! isCritic ) {
            _text.UpdateColour( defaultColor );
            _anim.SetTrigger( "Simple" );
        } else {
            _text.UpdateColour( criticalColor );
            _anim.SetTrigger( "Critic" );
        }
    }

    /// <summary> 
    /// Init class method.
    /// </summary>
    private void Init() {

        // get text component reference.
        if ( _text == null ) {
            _text = GetComponent<TextComponent>();
        }

        // get animator component reference.
        if ( _anim == null ) {
            _anim = GetComponent<Animator>();
        }
    }
}
