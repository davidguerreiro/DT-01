using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour {
    public AudioClip[] audioClips;                              // Audioclips to use with this audio component instance.

    [HideInInspector]
    public AudioSource audio;                                   // Audio component reference.
    [HideInInspector]
    public bool onFade;                                         // Flag to control the system is on fade coroutine.
    private int currentAudioClipIndex;                          // Current audio clip index assigned to the audio clip.
    private Coroutine fadeCoroutine;                            // Fade song coroutine.
    private float initVolume;                                   // Init song volume.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Play sound.
    /// </summary>
    /// <param name="soundClip">int - sound clip to play from the array of sounds.</param>
    public void PlaySound( int audioClip ) {

        audio.clip = audioClips[ audioClip ];
        this.currentAudioClipIndex = audioClip;
        
        audio.Play();
    }

    /// <sumamry>
    /// Set audio clip passed
    /// by parameter.
    /// </summary>
    /// <param name="clip">AudioClip - new audio clip to play.</param>
    /// <param name="autoPlay">bool - true by default. Wheter to play the audio now or later</param>
    public void PlayClip( AudioClip clip, bool autoPlay = true ) {

        audio.clip = clip;

        if ( autoPlay ) {
            audio.Play();
        }
    }
    /// <summary>
    /// Play current sound.
    /// </summary>
    public void PlaySound() {

        if ( audio.isPlaying ) {
            audio.Stop();
        }

        audio.Play();

    }

    /// <summary>
    /// Stop audio sound.
    /// </summary>
    public void StopAudio() {
        audio.Stop();
    }

    /// <summary>
    /// Pause audio.
    /// </summary>
    public void PauseAudio() {
        audio.Pause();
    }

    /// <summary>
    /// Get current audio clip. Returns array
    /// index by default.
    /// </summary>
    /// <param name="returnsClipName">bool - optional - Returns clip name instead</param>
    /// <returns>string</returns>
    public string GetCurrentAudioClip( bool returnsClipName = false ) {
        if ( returnsClipName ) {
            return audio.clip.name;
        } else {
            return this.currentAudioClipIndex.ToString();
        }
    }

    /// <summary>
    /// Set current audio clip
    /// index.
    /// </summary>
    /// <param name="index">int - which index is new current audioClip</param>
    public void SetCurrentAudioClip( int index ) {
        
        if ( index < audioClips.Length && index >= 0 ) {
            this.currentAudioClipIndex = index;
        }
    }

    /// <summary>
    /// Fade out song.
    /// Song will stop gradually.
    /// </summary>
    /// <param name="speed">float - fade out sound speed. Default to 1f</param>
    /// <param name="toPause">bool - song will be paused if true. Otherwise stopped. False by default</param>
    /// <param name="restartVolume">bool - whether to restart volume to original value after the songs fades out</param>
    /// <returns>IEnumerator</retruns>
    private IEnumerator FadeOutSongRoutine( float speed = 1f, bool toPause = false, bool restartVolume = true ) {
        onFade = true;
         
        while ( audio.volume > 0f ) {
            audio.volume -= speed * Time.deltaTime;
            yield return new WaitForFixedUpdate(); 
        }

        if ( toPause ) {
            audio.Pause();
        } else {
            audio.Stop();
        }

        if ( restartVolume ) {
            audio.volume = initVolume;
        }

        onFade = false;
    }

    /// <summary>
    /// Fade in song.
    /// Song will start gradually.
    /// </summary>
    /// <param name="speed">float - fade out sound speed. Default to 1f</param>
    /// <param name="startVolume">float - Initial volume. Default to 0f</param>
    /// <param name="maxVolume">float - maximun volume to reaach.Default to 1f</param>
    /// <param name="selectClip">bool - whether to select another clip in the array to play. Default to false</param>
    /// <param name="clip">int - Clip to play. Only works if selectClip is set to true. Default to 0</param>
    /// <returns>IEnumerator</retruns>
    private IEnumerator FadeInSongRoutine( float speed = 1f, float startVolume = 0f, float maxVolume = 1f, bool selectClip = false, int clip = 0 ) {
        onFade = true;
        audio.volume = startVolume;

        if ( selectClip ) {
            PlaySound( clip );
        } else {
            PlaySound();
        }
         
        while ( audio.volume < maxVolume ) {
            audio.volume += speed * Time.deltaTime;
            yield return new WaitForFixedUpdate(); 
        }

        onFade = false;
    } 

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioSource>();

        // set default audio clip value.
        this.currentAudioClipIndex = 0;

        // get init volume.
        initVolume = audio.volume;
    }
}
