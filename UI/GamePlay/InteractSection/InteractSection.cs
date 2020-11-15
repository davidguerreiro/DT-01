using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractSection : MonoBehaviour {
    public int currentID = -1;                              // Current interactable associated ID.

    [Header("Status")]
    public bool displayed;                                  // Flag to control display status.
    public bool inProcess;                                  // This flag is true whenever the player is interacting with this interactable.                     

    [Header("Components")]
    public TextComponent actionLabelText;                   // Action label text component.
    public Image fillImage;                                 // Fill image component reference.

    private string _label;                                  // Label to display in action label text.
    private string _labelProgress;                          // Label to display in action label text when the player is interacting with this interactable.
    private bool _innmediate;                               // If true, this event will be performed inmediateley.
    private float _fillSpeed;                               // Fill speed used to calculate how long it will take the player to complete this action.
    private Animator _anim;                                 // Animator component reference.
    private Coroutine _actionRoutine;                       // Action coroutine reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Display initeract
    /// section label.
    /// </summary>
    public void Display() {
        _anim.SetBool( "Displayed", true );
        displayed = true;
    }

    /// <summary>
    /// HIde interact
    /// section label.
    /// </summary>
    public void Hide() {
        _anim.SetBool( "Displayed", false );
        displayed = false;
    }

    /// <summary>
    /// Set up label component.
    /// Data coming from interactable
    /// element.
    /// </summary>
    /// <parma name="id">int - interactable id</param>
    /// <param name="label">string - label to be displayed</param>
    /// <param name="labelAction">string - label to be displayed during action.</param>
    /// <parma name="fillSpeed">float - action completion speed.</param>
    /// <param name="inmediateEvent">bool - wheter this event is to happen just after the action key is pressed ( no hold key )</parmam>
    public void SetUp( int id, string label, string labelAction, float fillSpeed, bool inmediateEvent = false ) {
        if ( currentID != id ) {
            currentID = id;
            this._label = label;
            this._labelProgress = labelAction;
            this._fillSpeed = fillSpeed;
            this._innmediate = inmediateEvent;

            actionLabelText.UpdateContent( label );
        }

        if ( ! displayed ) {
            Display();
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
