using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : Collectable {
    public PlayerStats playerStats;                     // Data source, used to update player amount of shards.
    public int value;                                   // How many shards collected by this shard.
    public GameObject model;                            // 3D shard model.

    /// <summary>
    /// Collected.
    /// Update player's total number of shards.
    /// </summary>
    public void Collect() {
        DisableModels();

        playerStats.UpdateShards( value );
    }

    /// <summary>
    /// Disable child gameobjects.
    /// Use when collecting the shard.
    /// </summary>
    private void DisableModels() {
        model.SetActive( false );
    }
}
