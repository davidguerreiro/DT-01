using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{
    [Header("Status")]
    public bool displayed;                                  // Flag to contorle whether the dialogue box is displayed in screen.
    public bool playing;                                    // Playing dialogue coroutine.

    [Header("Data Source")]
    public DialogueContent content;                         // Dialogue content.

    [Header("Components")]
    public DialogueActorName actorName;                      // Actor name component.
    public DialogueText text;                                // Dialogue text component.

    [HideInInspector]
    public Animator anim;                                    // Animator component reference.

    private AudioComponent audio;                           // Audio component reference.
    private string currentSpeaker;                          // Current dialogue speaker.
    private Coroutine dialogueRoutine;                      // Dialogue routine reference.
    private bool playNext;                                  // Flag to control whether to display next dialogue text.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    /// Play scene dialogue
    /// coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator PlayDialogue() {
        playing = true;
        Display();

        for ( int i = 0; i < content.dialogue.Length; i++ ) {
            playNext = false;
            
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
            }

            // display dialogue in dialogue box.

        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioComponent>();
    }
}
