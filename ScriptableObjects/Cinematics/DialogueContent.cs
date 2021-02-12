using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContent : ScriptableObject {
    
    [Serializable]
    public struct Dialogue {
        public string speaker;                  // Speaker name.
        [TextArea(0, 15)]
        public string content;                  // Dialogue content.
    }

    public Dialogue[] dialogue;                 // Dialogue reference.
}
