using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour {

    public bool InDialogue;                                 // Flag to control whether dialogue is displayed.

    [Header("Data Source")]
    public DialogueContent content;                         // Dialogue content.

    [Header("Components")]
    public TextComponent actorName;                         // Actor name component.
    public TextComponent dialogueText;                      // Dialogue text component.
    public Animator anim;                                   // Animator component reference.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
