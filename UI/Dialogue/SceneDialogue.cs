using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{
    [Header("Status")]
    public bool displayed;                                  // Flag to contorle whether the dialogue box is displayed in screen.
    public bool playing;                                    // Playing dialogue coroutine.

    [Header("Components")]
    public DialogueActorName actorName;                      // Actor name component.
    public DialogueText text;                                // Dialogue text component.

    [HideInInspector]
    public Animator anim;                                    // Animator component reference.

    private DialogueContent content;                         // Dialogue content.
    private AudioComponent audio;                           // Audio component reference.
    private string currentSpeaker;                          // Current dialogue speaker.
    private Coroutine dialogueRoutine;                      // Dialogue routine reference.
    private bool playNext;                                  // Flag to control whether to display next dialogue text.

    // Start is called before the first frame update
    void Start() {
        Init();
    }
    
    /// <summary>
    /// Set up new dialogue content.
    /// </summary>
    /// <param name="newContent">DialogueContent - New dialogue content</param>
    public void SetUpContent(DialogueContent newContent) {
        this.content = newContent;
    }

    /// <summary>
    /// Display dialogue box.
    /// </summary>
    public void Display() {
        anim.SetBool("Display", true);
        displayed = true;
    }

    /// <summary>
    /// Hide dialogue box.
    /// </summary>
    public void Hide() {
        anim.SetBool("Display", false);
        displayed = false;
    }

    /// <summary>
    /// Play dialogue.
    /// </summary>
    /// <param name="newContent">DialogueContent - new content scritable object.Null by default.</param>
    public void PlayDialogue(DialogueContent newContent = null) {
        if (!playing && dialogueRoutine == null) {
            if (newContent != null) {
                SetUpContent(newContent);
            }

            dialogueRoutine = StartCoroutine(PlayDialogueRoutine());
        }
    } 

    /// <summary>
    /// Play scene dialogue
    /// coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlayDialogueRoutine() {
        playing = true;
        bool displayCursor = false;
        Display();

        for ( int i = 0; i < content.dialogue.Length; i++ ) {
            playNext = false;
            displayCursor = ((i + 1) == content.dialogue.Length) ? false : true;
            
            // display first speaker.
            if ( i == 0 ) {
                actorName.Display(content.dialogue[i].speaker);
                currentSpeaker = content.dialogue[i].speaker;
            }

            // clear box.
            if ( i > 0 ) {
                text.Clear();   
            }

            // check if another actor is talking.
            if ( content.dialogue[i].speaker != currentSpeaker ) {
                actorName.actorFade.FadeOut();
                yield return new WaitForSeconds(.5f);
                actorName.Display(content.dialogue[i].speaker);
                currentSpeaker = content.dialogue[i].speaker;
                displayCursor = false;
            }

            // display dialogue in dialogue box.
            text.DisplayDialogueText(content.dialogue[i].content, displayCursor);

            do {
                // check if the user interacts during the dialogue.
                if ( Input.GetKeyDown("space") || Input.GetKeyDown("enter") || Input.GetMouseButtonDown(0) ) {

                    if (text.displayingDialogue) {
                        text.DisplayAll(content.dialogue[i].content, displayCursor);
                    } else {
                        audio.PlaySound();
                        playNext = true;
                    }
                }

            } while (!playNext);
            
        } // endforloop.

        Hide();
        playing = false;
        dialogueRoutine = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioComponent>();
    }
}
