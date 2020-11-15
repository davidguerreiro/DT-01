using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : ScriptableObject {
    public int id;                                          // Interactable id.

    [Header("Labels")]
    public string labelEn;                                  // English label used when showing interactable UI element.
    public string labelProgressEn;                          // English label used when the player is interacting with this element.
    public string labelEs;                                  // Spanish label used when showing interactable UI element.
    public string labelProgressEs;                          // Spanish label used when the player is interacting with this element.

    [Header("Properties")]
    public bool inmediate;                                  // Wheter this is an inmediate event. Inmediate event are enabled only pressing the action key one. Otherwise must be hold.
    public float actionSpeed;                               // How long it wil take the player to complete this action.
}
