using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssignable : ScriptableObject {
    public string keyAssociated;                        // Which key is used to trigger the assignated item usage method.
    public ItemData itemData;                           // Assignable item data reference.
}
