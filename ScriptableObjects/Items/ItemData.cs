using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject {
    public int id;                                              // Item id.

    [Header("Basic data")]
    public string itemName_en;                                  // Item name in English.
    public string itemName_es;                                  // Item name in Spanish.
    public Sprite sprite;                                       // Item 2D sprite.
    public enum Type {
        basic,                                                  // Generic standard items.
        crafting,                                               // Using to build new items, to be sold in shops or to be traded with other NPCs.
        important,                                              // Key items, related with the story or with gears which improves stats in a passive way. They cannot be used, cannot be attached and cannot be sold or traded.
    }

    public Type type;                                           // Item type.

    [Header("Attributes")]
    public bool stackable;                                      // If true, this item will be added to the inventory when collected.
    public bool usable;                                         // If true, this item can be attachable to usable items inventory, and thus can be used by the player.
    public bool sellable;                                       // If true, this item can be sold at shops or traded with other NPCs.
    public int price;                                           // Price to pay when this item is bought in a shop. Cut by half when selling it.
    public int maxStack;                                        // Maximun inventory stack for this item. Not applicable to non-stackable items.
    
    
    [Header("Description")]
    [TextArea]
    public string description_en;                               // Item description in English.
    [TextArea]
    public string description_es;                               // Item description in Spanish.

    [Header("Media")]
    public AudioClip itemUsageSound;                            // Item usage sound audio clip. Not required.
    public AudioClip itemFailedSound;                           // Item wrong or failed sound audio clip. Not required.

}
