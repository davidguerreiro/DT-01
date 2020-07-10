using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    public enum WalkingSurfaces {                                    // Walking surfaces enum list - used to display player walking sound.
        Ground = 0,
        Metal = 1,
    };

    private FPSInput _playerInput;                                   // Player input class.
    public WalkingSurfaces currentSurface;                          // Current surface the player is walking into. Used to display different sound when walking.
    private AudioComponent _audioComponent;                          // Audio component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
        // play walking sound if player is moving.
        PlayWalkingSound();
    }

    private void OnTriggerExit(Collider other) {
        if ( other.gameObject.layer == LayerMask.NameToLayer( "Ground" ) ) {
            _playerInput.grounded = false;
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other ) {

        // check if player is grounded.
        if ( other.gameObject.layer == LayerMask.NameToLayer( "Ground" ) ) {
            _playerInput.grounded = true;

            // check to which surface the player is grounded.
            CheckSurfaceType( other.gameObject );
        }
    }

    private void OnTriggerStay(Collider other) {

        // check if player is grounded.
         if ( other.gameObject.layer == LayerMask.NameToLayer( "Ground" ) ) {
            _playerInput.grounded = true;

            // check to which surface the player is grounded.
            CheckSurfaceType( other.gameObject );
        }   
    }

    /// <summary>
    /// Check surface type.
    /// Used to play different sound when moving.
    /// </summary>
    /// <param name="surfaceObject">GameObject - surface game object</param>
    private void CheckSurfaceType( GameObject surfaceObject ) {

        switch ( surfaceObject.tag ) {
            case "GroundSurface":
                currentSurface = WalkingSurfaces.Ground;
                break;
            case "MetalSurface":
                currentSurface = WalkingSurfaces.Metal;
                break;
            default:
                currentSurface = WalkingSurfaces.Ground;
                break;
        }
    }

    /// <summary>
    /// Play walking sound.
    /// </summary>
    private void PlayWalkingSound() {

        if ( _playerInput.grounded && _playerInput.isMoving ) {

            if ( ! _audioComponent.audio.isPlaying ) {

                int surfaceSoundKey = (int) currentSurface;
                _audioComponent.PlaySound( surfaceSoundKey );
            }
        } else {
            _audioComponent.PauseAudio();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get player input script component reference.
        _playerInput = GetComponentInParent<FPSInput>();

        // get audio component.
        _audioComponent = GetComponent<AudioComponent>();

        // set default value for walking surface.
        currentSurface = WalkingSurfaces.Ground;
    }
}
