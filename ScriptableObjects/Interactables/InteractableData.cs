using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : ScriptableObject {
    public string label;                                    // Label using when showing interactable UI element.
    public bool inmediate;                                  // Wheter this is an inmediate event. Inmediate event are enabled only pressing the action key one. Otherwise must be hold.
    public float actionSpeed;                               // How long it wil take the player to complete this action.
}
