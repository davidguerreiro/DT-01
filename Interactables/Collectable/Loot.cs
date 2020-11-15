using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    [Serializable]
    public struct LootItems {                           // Data structure for loot items.
        public GameObject item;                         // Item ( usually prefab ) reference to be dropped.
        public bool forceDrop;                          // If true, this item will always be dropped, but if there are others with force drop enable, ensure there is a min amount set to cover all. 
        public int dropRate;                            // Item drop rate - numbers between 0 - 100.
    };

    [Header( "Loot Items")]
    public LootItems[] loot = new LootItems[1];         // Loot reference.

    [Header( "Settings" )]
    public bool hasForced;                              // Set this to true to ensure force drop items are included in the loot array before runnign the algorythim to set the random
    public bool dropOnlyForced;                         // Set this to true to drop only force items.
    public int minDrop = 1;                             // Minimun of items dropped by this enemy / destructible.
    public int maxDrop = 2;                             // Maxumun of items dropped by this enemy / destructible.
    public float heightDrop = 0.1f;                      // Height at where the dropped item will be displayed in the game scene.
    public float xVariableDistance = 0.1f;               // X Variable distance used to randomize X position.
    public float zVariableDistance = 0.1f;               // Z Variable distance used to randomize Z position.

    private List<GameObject> forced;                     // Forced items to be dropped are kept here.           

    /// <summary>
    /// Generate loot array of gameObjects.
    /// </summary>
    /// <returns>GameObject[]</returns>
    private GameObject[] GenerateLoot() {

        int totalLoot = UnityEngine.Random.Range( minDrop, maxDrop + 1 );
        GameObject[] lootToDrop = new GameObject[ totalLoot ];

        // add force drop items in the loop list.
        if ( hasForced ) {
            AddForceDropItems( ref lootToDrop );

            if ( dropOnlyForced ) {
                return lootToDrop;
            }
        }
        

        // check if we shuffle the loop array. Shuffle the array will increase rng chances
        // and avoid items above in the array to drop more times.
        if ( loot.Length > 1 && UnityEngine.Random.Range( 0, 2 ) == 0 ) {
            Utils.instance.Randomize( loot );
        }

        // generate loot to drop
        // first pick up randomly a drop item from the list, the trigger
        // percentage algorithm.
        for ( int i = 0; i < lootToDrop.Length; i++ ) {

            int dropItemKey = 0;
            int counter = 0;
            float chance = 0f;

            LootItems itemToDropData;
            GameObject itemToDrop = null;

            // set item to drop.
            do {
                
                // set item to drop.
                dropItemKey = UnityEngine.Random.Range( 0, loot.Length );

                itemToDropData = loot[ dropItemKey ];
                
                // ignore force drops in the algorythim.
                if ( itemToDropData.forceDrop ) {
                    continue;
                }

                chance = 100f - itemToDropData.dropRate;

                // calculate if object is droped.
                if ( chance < UnityEngine.Random.Range( 0f, 101f ) ) {
                    itemToDrop = itemToDropData.item;
                }

                counter++;
                
                // assign first item to be dropped in case no item has dropped in this slot after 30 tries.
                if ( counter == 30 ) {
                    itemToDrop = loot[ dropItemKey ].item;
                }

            } while ( itemToDrop == null );

            lootToDrop[ i ] = itemToDrop;
        }

        return lootToDrop;
    }

    /// <summary>
    /// Add force drop items into
    /// lootDrop array.
    /// </summary>
    /// <param name="lootToDrop">GameObject[] - Loot to drop array. Used to add force drop items before running the algorythim to calculate random loot. Passed by reference.</param>
    private void AddForceDropItems( ref GameObject[] lootToDrop ) {
        int j = 0;
        for ( int i = 0; i < lootToDrop.Length; i++ ) {
            if ( loot[ i ].forceDrop ) {
                lootToDrop[ j ] = loot[ i ].item;
                j++;
            }
        }
    }

    /// <summary>
    /// Drop loot into the game scene.
    /// </summary>
    /// <param name="applyForce">bool - if true, the drop items will receibe a force boost before dropping.</param>
    public void DropLoot( bool applyForce = false ) {

        // get loot.
        GameObject[] itemsToDrop = GenerateLoot();

        float xVariation;
        float zVariation;
        
        for ( int i = 0; i < itemsToDrop.Length; i++ ) {

            // calculate drop variations.
            xVariation = UnityEngine.Random.Range( transform.localPosition.x - xVariableDistance, transform.localPosition.x + ( xVariableDistance + .01f ) );
            zVariation = UnityEngine.Random.Range( transform.localPosition.z - zVariableDistance, transform.localPosition.z + ( zVariableDistance + .01f ) );

            // instance loot.
            Vector3 instanceLocalPosition = new Vector3( transform.localPosition.x + ( xVariation / 10f ), transform.localPosition.y + heightDrop, transform.localPosition.z + ( zVariation / 10f ) );
            GameObject instance = Instantiate( itemsToDrop[ i ], transform.localPosition, transform.rotation );

            // set loot position and remove parent to avoid loot dissapearing with the dropper gameObject.
            instance.transform.parent = this.gameObject.transform;
            instance.transform.localPosition = instanceLocalPosition;
            instance.transform.parent = null;

            if ( applyForce ) {
                instance.GetComponent<Rigidbody>().AddForce( new Vector3( 0f, 10f, 10f ) );
            }
        }
    }
}
