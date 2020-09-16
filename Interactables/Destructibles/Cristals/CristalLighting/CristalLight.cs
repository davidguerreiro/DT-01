using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalLight : MonoBehaviour {
    private Animator _anim;                                 // Animator compontn reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Switch light off.
    /// </summary>
    public void SwitchOff() {
        
        if ( _anim != null ) {
            _anim.SetTrigger( "LightOff" );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        _anim = GetComponent<Animator>();
    }
}
