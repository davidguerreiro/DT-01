using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{
    [Header("Status")]
    public bool displayed;                                  // Flag to contorle whether the dialogue box is displayed in screen.

    [Header("Data Source")]
    public DialogueContent content;                         // Dialogue content.

    [Header("Components")]
    public DialogueActorName actorName;                      // Actor name component.
    public DialogueText text;                                // Dialogue text component.

    [HideInInspector]
    public Animator anim;                                    // Animator component reference.

    // Start is called before the first frame update
    void Start()
    {
        
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
    /// Init class method.
    /// </summary>
    private void Init() {
        anim = GetComponent<Animator>();
    }
}
