using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour {

    [Header("Status")]
    public bool displayingDialogue;                          // Flag to control whether we are in a dialogue.

    [Header("Components")]
    public GameObject cursor;                               // Cursor gameObject reference.
    private TextComponent text;                             // Dialogue text component.

    private Coroutine dialogueCoroutine;                    // Dialogue coroutine.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Displays dialogue in the dialogue text.
    /// </summary>
    /// <param name="content">string - text content to display</param>
    /// <param name="displayCursor">bool - wheter to display cursor when the text is displayed. True by default</param>
    public void DisplayDialogueText(string content, bool displayCursor) {
        if ( ! displayingDialogue && dialogueCoroutine == null ) {
            dialogueCoroutine = StartCoroutine(DisplayDialogueTextRoutine(content, displayCursor));
        }
    }

    /// <summary>
    /// Displays current dialogue
    /// content coroutine.
    /// </summary>
    /// <param name="content">string - text content to display</param>
    /// <param name="displayCursor">bool - wheter to display cursor when the text is displayed. True by default</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator DisplayDialogueTextRoutine(string content, bool displayCursor = true) {
        displayingDialogue = true;        
        string currentText;
        char[] dialogueLetters;

        if (cursor.activeSelf == true ) {
            cursor.SetActive(false);
        }

        dialogueLetters = content.ToCharArray();

        for ( int j = 0; j < dialogueLetters.Length; j++ ) {
            currentText = text.GetContent();
            if (currentText == "") {
                currentText = dialogueLetters[j].ToString();
            } else {
                currentText += dialogueLetters[j].ToString();
            }
            text.UpdateContent(currentText);
            yield return new WaitForSeconds(.05f);
        }

        if ( displayCursor ) {
            cursor.SetActive(true);
        }
        
        displayingDialogue = false;
        dialogueCoroutine = null;
    }

    /// <summary>
    /// Display all text without animation.
    /// </summary>
    /// <param name="content">string - text content to display</param>
    /// <param name="displayCursor">bool - wheter to display cursor when the text is displayed. True by default</param>
    public void DisplayAll(string content, bool displayCursor = true) {
        if ( displayingDialogue && dialogueCoroutine != null ) {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
            displayingDialogue = false;
        }

        text.UpdateContent(content);

        if (displayCursor) {
            cursor.SetActive(true);
        }
    }

    /// <summary>
    /// Clear dialogue box.
    /// </summary>
    public void Clear() {
        text.UpdateContent("");
        cursor.SetActive(false);
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        text = GetComponent<TextComponent>();
    }
}
