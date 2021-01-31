using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicController : MonoBehaviour
{   
    public string currentSong;

    [Serializable]
    public struct SongData {                                // Songs data
        public string name;         // Song name.
        public AudioClip clip;      // Song clip.
    }
    public SongData[] commonTracks;                         // Common tracks array.
    public SongData[] sceneTracks;                          // Scene tracks array.
    private Hashtable _commonTracks = new Hashtable();      // Common tracks for level. Usually standard music and battle music.
    private Hashtable _sceneTracks = new Hashtable();       // Scene tracks. Usually tracks used in scenes and events.
    private AudioComponent _audio;                          // Audio component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Add song to level music controller.
    /// </summary>
    /// <param name="listName">string - list name ID to add the clip.</param>
    /// <param name="key">string - clip key</param>
    /// <param name="clip">AudioClip = clip reference</param>
    public void AddSong( string listName, string key, AudioClip clip ) {
        switch( listName ) {
            case "common":
                _commonTracks.Add(key, clip);
                break;
            case "scene":
                _sceneTracks.Add(key, clip);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Update song to level music controller.
    /// </summary>
    /// <param name="listName">string - list name ID to add the clip.</param>
    /// <param name="key">string - clip key</param>
    /// <param name="clip">AudioClip = clip reference</param>
    public void UpdateSong( string listName, string key, AudioClip clip ) {
        switch( listName ) {
            case "common":
                _commonTracks[key] = clip;
                break;
            case "scene":
                _sceneTracks[key] = clip;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Delete song from level music controller.
    /// </summary>
    /// <param name="listName">string - list name ID to add the clip.</param>
    /// <param name="key">string - clip key</param>
    public void DeleteSong( string listName, string key, AudioClip clip ) {
        switch( listName ) {
            case "common":
                _commonTracks.Remove(key);
                break;
            case "scene":
                _sceneTracks.Remove(key);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Play level song.
    /// </summary>
    /// <param name="listName">string - list name ID to add the clip.</param>
    /// <param name="key">string - clip key</param>
    /// <param name="inmediateStart">bool - wheter to start the song inmediately or fade songs.True by default.</param>
    public void PlaySong( string listName, string key, bool innmediateStart = true ) {
        switch( listName ) {
            case "common":
                StartCoroutine( ReproduceSong((AudioClip)_commonTracks[key], innmediateStart) );
                break;
            case "scene":
                StartCoroutine( ReproduceSong((AudioClip)_commonTracks[key], innmediateStart) );
                break;
            default:
                break;
        }
        currentSong = key;
    }

    /// <summary>
    /// Reproduce song.
    /// </summary>
    /// <param name="clip">AudioClip - audio clip to reproduce</param>
    /// <param name="inmediateStart">bool - wheter to start the song inmediately or fade songs.</param>
    private IEnumerator ReproduceSong( AudioClip clip, bool inmediateStart ) {
        if (_audio != null ) {
            if ( inmediateStart ) {
                _audio.PlayClip( clip );
            } else {
                StartCoroutine( _audio.FadeOutSongRoutine( 0.3f, false ) );
                yield return new WaitForSecondsRealtime( 2.5f );
                _audio.PlayClip( clip, false );
                StartCoroutine( _audio.FadeInSongRoutine( 0.7f, 0f, .5f ) );
            }
        }
    }

    /// <summary>
    /// Stop current song.
    /// </summary>
    /// <param name="inmediateStop">bool - wheter to stop the song inmediately or to fade stop. True by default</param>
    public void StopSong( bool inmediateStart = true ) {
        if ( inmediateStart ) {
            _audio.PauseAudio();
        } else {
            StartCoroutine( _audio.FadeOutSongRoutine( .5f ) );
        }
    }

    /// <summary>
    /// Play common level song.
    /// </summary>
    /// <param name="inmediateStart">bool - wheter to start the song inmediately</param>
    public void PlayCommonLevelSong( bool inmediateStart = false ) {
        PlaySong("common", "currentLevel", inmediateStart);
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {
        _audio = GetComponent<AudioComponent>();

        // build commont tracks hastable.
        if ( commonTracks.Length > 0 ) {
            foreach ( LevelMusicController.SongData data in commonTracks ) {
                _commonTracks.Add( data.name, data.clip );
            }
        }

        // build scene tracks hastable.
        if ( sceneTracks.Length > 0 ) {
            foreach( LevelMusicController.SongData data in sceneTracks ) {
                _sceneTracks.Add( data.name, data.clip );
            }
        }
    }
}
