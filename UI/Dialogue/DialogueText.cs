using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour {

    [Header("Status")]
    public bool inDialogue;                                 // Flag to control whether we are in a dialogue.
    public bool displayed;                                  // Flag to contorle whether the dialogue box is displayed in screen.

    [Header("Data Source")]
    public DialogueContent content;                         // Dialogue content.

    [Header("Components")]
    public DialogueActorName actorName;                      // Actor name component.
    public TextComponent dialogueText;                      // Dialogue text component.
    public Animator anim;                                   // Animator component reference.
    public GameObject cursor;                               // Cursor gameObject reference.

    private Coroutine dialogueCoroutine;                    // Dialogue coroutine.
    private int dialogueKey;                                // Key to keep track of current dialogue.

    // Update is called once per frame
    void Update() {
        
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
    /// Displays current dialogue
    /// content coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator PlayDialogueRoutine() {
        string currentText;
        char[] dialogueLetters;

        // hide cursor if required.
        if ( cursor.activeSelf ) {
            cursor.SetActive(false);
        }

        // display actor name.
        if ( !actorName.actorFade.displayed ) {
            actorName.Display(content.dialogue[dialogueKey].speaker);
        } else {
            actorName.displayedName.UpdateContent(content.dialogue[dialogueKey].speaker);
        }

        dialogueLetters = content.dialogue[dialogueKey].content.ToCharArray();

        for ( int j = 0; j < dialogueLetters.Length; j++ ) {
            currentText = dialogueText.GetContent();
            currentText += (currentText + dialogueLetters[j]).ToString();
            dialogueText.UpdateContent(currentText);
            yield return new WaitForSeconds(.5f);
        }

        dialogueKey++;

        if ( dialogueKey < content.dialogue.Length ) {
            cursor.SetActive(true);
        }
        
        dialogueCoroutine = null;
    }
}
