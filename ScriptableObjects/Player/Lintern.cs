using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lintern : ScriptableObject {
    [Header("Lintern Status")]
    public bool enabled = false;                         // Flag to check whether the lintern is being used by the player or not.
    public bool isCharging = false;                      // Flag to check whether the lintenr battery is being charged.

    [Header("Battery")]
    public int currentBattery;                           // How much battery the lintern currently has.
    public int maxBattery;                               // Maximun lintern capacity.

    [Header("Charge Speed")]
    public float dischargeSpeed;                         // Rate used to calculate how fast the battery is drained when the lintern is in usage by the player.
    public float chargeSpeed;                            // Rate used to calculate how fast the lintern battery recharges when the player is not using the lintern.                             

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
