using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    [Serializable]
    public struct LootItems {                           // Data structure for loot items.
        public GameObject item;                         // Item ( usually prefab ) reference to be dropped.
        public int dropRate;                            // Item drop rate - numbers between 0 - 100.
    };

    [Header( "Loot Items")]
    public LootItems[] loot = new LootItems[1];         // Loot reference.

    [Header( "Settings" )]
    public int minDrop = 1;                             // Minimun of items dropped by this enemy / destructible.
    public int maxDrop = 2;                             // Maxumun of items dropped by this enemy / destructible.
    public float heightDrop = 50f;                      // Height at where the dropped item will be displayed in the game scene.
    public float xVariableDistance = 15f;               // X Variable distance used to randomize X position.
    public float zVariableDistance = 15f;               // Z Variable distance used to randomize Z position.                


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Generate loot array of gameObjects.
    /// </summary>
    /// <returns>GameObject[]</returns>
    private GameObject[] GenerateLoot() {

        int totalLoot = UnityEngine.Random.Range( minDrop, maxDrop );
        GameObject[] lootToDrop = new GameObject[ totalLoot ];

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
    /// Drop loot into the game scene.
    /// </summary>
    /// <param name="origin">Transform - origin point to calculate drop item position</param>
    public void DropLoot( Transform origin ) {

        GameObject[] itemsToDrop = GenerateLoot();

        float xVariation;
        float zVariation;
        
        for ( int i = 0; i < loot.Length; i++ ) {

            // calculate drop variations.
            xVariation = UnityEngine.Random.Range( origin.position.x - xVariableDistance, origin.position.x + ( xVariableDistance + 1f ) );
            zVariation = UnityEngine.Random.Range( origin.position.z - zVariableDistance, origin.position.z + ( zVariableDistance + 1f ) );

            // instance loot.
            Instantiate( itemsToDrop[ i ], new Vector3( origin.position.x + xVariation, origin.position.y + heightDrop, origin.position.z + zVariation ), Quaternion.identity );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        
    }
}
