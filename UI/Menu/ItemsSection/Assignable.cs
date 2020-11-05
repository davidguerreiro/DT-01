using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assignable : MonoBehaviour {

    public bool empty;                                      // Whether this assignable slot is ready to get an item assigned.
    
    // [Header("Assignable data")]
    // TODO: Call scriptable from here.

    [Header("Components")]
    public Image itemImage;                                 // Item image displayed in this assignable component.

    private Animator _anim;                                 // Anim component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        if ( _anim != null ) {
            _anim = GetComponent<Animator>();
        }
    }
}
