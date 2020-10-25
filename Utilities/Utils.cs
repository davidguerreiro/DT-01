using System.Collections;
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
            LoadDebugMenu();
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
    /// Init class method.
    /// </summary>
    private void Init() {

        // get standard shared materials utils class reference.
        standardSharedUtils = GetComponent<StandardShaderUtils>();
    }
}
