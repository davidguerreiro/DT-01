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

    public Data[] textData;                             // Text data public reference.
    
    /// <summary>
    /// Get text data by attribute
    /// </summary>
    /// <param name="attributeValue">string - attribute value</param>
    public Data GetDataElement( string attributeValue ) {
        Data returned = new Data();

        foreach ( Data textDataItem in textData ) {
            if ( textDataItem.attribute == attributeValue ) {
                returned = textDataItem;
                break;
            }
        }
        
        return returned;
    }
    /// <summary>
    /// Clean up in use values.
    /// </summary>
    public void CleanUp() {
        for ( int i = 0; i < textData.Length; i++ ) {
            textData[i].inUse = false;
        }
    }
}
