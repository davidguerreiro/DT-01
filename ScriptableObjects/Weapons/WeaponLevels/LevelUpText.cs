using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpText : ScriptableObject {
   [Serializable]
    public struct Data {
        public string attribute;                        // To which attribute this text belongs to.
        public bool inUse;                              // Whether to display this attribute.
        public string enValue;                          // English text value.
        public string esValue;                          // Spanish text value.
    }

    public Data[] textData;
    
}
