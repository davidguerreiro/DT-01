using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuContent : MonoBehaviour {

    [HideInInspector]
    public int currentID = 0;                                   // Current active section ID.
    public MenuContentSection[] sections;                       // Menu sections reference.

    [Header("Settings")]
    public float waitInBetween = 1f;                            // How long between each section is hide and displayed.
    public int[] requireInit;                                     // Array which contains sections that require initialisation.                                 
    
    private Coroutine _animRoutine;                             // Switch section coroutine.
    private AudioComponent _audio;                              // Audio component reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Switch section wrapper.
    /// </summary>
    /// <param name="sectionID">int - section id</param>
    public void SwitchSection( int sectionID ) {
        if ( _animRoutine == null ) {
            _animRoutine = StartCoroutine( SwitchSectionRoutine( sectionID ) );
        }
    }

    /// <summary>
    /// Switch section coroutine.
    /// </summary>
    /// <param name="sectionID">int - section id</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SwitchSectionRoutine( int sectionID ) {
        _audio.PlaySound();

        // hide current section.
        sections[currentID].Hide();
        yield return new WaitForSecondsRealtime( waitInBetween );
        sections[currentID].gameObject.SetActive( false );

        // display new section.
        sections[sectionID].gameObject.SetActive( true );

        // run any initialization required on section.
        RunInitialisations( sectionID );
        sections[sectionID].Display();

        currentID = sectionID;
        _animRoutine = null;
    }

    /// <summary>
    /// Check if current section
    /// needs to run Initialisation
    /// when the menu opens.
    /// </summary>
    private void CheckForInit() {
        if ( requireInit.Contains( currentID ) ) {
            RunInitialisations( currentID );
        }
    }

    /// <sumamry>
    /// Init selected section if
    /// neccesary.
    /// </summary>
    /// <param name="sectionID">int - section id</param>
    private void RunInitialisations( int sectionID ) {
        switch( sectionID ) {
            case 3:
                sections[sectionID].gameObject.GetComponent<ItemsSections>().InitSection();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable() {
        CheckForInit();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    } 
}
