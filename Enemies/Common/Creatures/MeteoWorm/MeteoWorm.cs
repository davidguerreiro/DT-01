using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoWorm : Enemy {

    private Animator _anim;                             // Animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    

    /// <summary>
    /// Init class method.
    /// </summary>
    public override void Init() {
        base.Init();

        // get animator component reference.
        _anim = GetComponent<Animator>();
    }
}
