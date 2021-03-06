﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour {
    public static Utils instance;                   // Class public static instance - this is the unique instance of this class.

    [HideInInspector]
    public StandardShaderUtils standardSharedUtils; // Standard shared Utils class reference. This class contains methods to manipulate Standard Unity Shader materials.


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    // Update is called once per frame.
    void Update() {
        
        // Quit game when pressing escape key.
        if ( Input.GetKeyDown( "escape" ) ) {
            // QuitGame();
        }

        // Open debug menu.
        if ( Input.GetKeyDown( "p" ) ) {
           // LoadDebugMenu();
        }
    }

    /// <summary>
    /// Load debug menu.
    /// </summary>
    private void LoadDebugMenu() {
        SceneManager.LoadScene( "DebugMenu" );
    }

    /// <summary>
    /// Load scene.
    /// </summary>
    /// <param name="sceneName">string - scene name to be loaded.</param>
    public void LoadScene( string sceneName ) {
        SceneManager.LoadScene( sceneName );
    }

    /// <summary>
    /// Quit game.
    /// </summary>
    private void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Randomize array to improve
    /// algorithms based on RNG.
    /// </summary>
    /// <param name="items">array - items to shuffle.</param>
    public void Randomize<T>( T[] items ) {

        System.Random rand = new System.Random();

        // For each spor in the array, pick a random item to swap into that spot.
        for ( int i = 0; i < items.Length - 1; i++ ) {

            int j = rand.Next( i, items.Length );
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }

    /// <summary>
    /// Normalize value
    /// between 0 and 1.
    /// </summary>
    /// <param name="rawValue">float - raw value to normalise</param>
    /// <param name="min">float - min value</param>
    /// <param name="max">float - max value</param>
    /// <returns>float</returns>
    public float Normalize( float rawValue, float min, float max ) {
        return ( rawValue - min ) / ( max - min );
    }

    /// <summary>
    /// Disable editor only
    /// gameObjects.
    /// </summary>
    public void DisableEditorOnlyGameObejcts() {
        GameObject[] editorOnly = GameObject.FindGameObjectsWithTag("EditorOnly");
        foreach ( GameObject element in editorOnly ) {
            element.SetActive(false);
        }
    }

    /// <summary>
    /// Hide all checkpoints and area
    /// loaders in current scene.
    /// </summary>
    public void HideCheckpoints() {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("GameController");
        foreach ( GameObject checkpoint in checkpoints ) {
            checkpoint.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get standard shared materials utils class reference.
        standardSharedUtils = GetComponent<StandardShaderUtils>();

        // disable all editor only gameObjects.
        DisableEditorOnlyGameObejcts();

        // hide all mesh renderer for checkpoints and area loaders.
        HideCheckpoints();
    }
}
