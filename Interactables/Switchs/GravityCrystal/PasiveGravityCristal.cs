using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasiveGravityCristal : MonoBehaviour {
    public bool activated;                                      // Whether this pasive gravity cristal has already been activated.

    public GameObject[] gravitationalElements;                  // Gravity cristal gravitational elements.
    
    private Animator _animator;                                 // Animator component reference.
    private AudioComponent _audio;                              // Audio component reference.

    // TODO: Add enable and call animations method and create cinematic.


    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <sumamry>
    /// Light the cristal up.
    /// </summary>
    public void LightUp() {
        _animator.SetBool( "Enabled", true );
        _audio.PlaySound(1); 
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioComponent>();
    }
}
