using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lintern : ScriptableObject {
    [Header("Lintern Status")]
    public bool enabled = false;                         // Flag to check whether the lintern is being used by the player or not.
    public bool isCharging = false;                      // Flag to check whether the lintenr battery is being charged.

    [Header("Battery")]
    public float currentBattery;                           // How much battery the lintern currently has.
    public float maxBattery;                               // Maximun lintern capacity.

    [Header("Charge Speed")]
    public float dischargeSpeed;                         // Rate used to calculate how fast the battery is drained when the lintern is in usage by the player.
    public float chargeSpeed;                            // Rate used to calculate how fast the lintern battery recharges when the player is not using the lintern.                             

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
        if ( enabled ) {
            DischargeBattery();
        } else {
            ChargeBattery();
        }
    }

    /// <summary>
    /// Discharge battery when it is in use.
    /// </summary>
    private void DischargeBattery() {

        if ( currentBattery > 0f ) {
            currentBattery -= dischargeSpeed / Time.deltaTime;

            if ( currentBattery < 0f ) {
                currentBattery = 0f;
            }
        }
    }

    /// <summary>
    /// Charge battery when it is not
    /// in use by the player.
    /// </summary>
    private void ChargeBattery() {
        
        if ( currentBattery < maxBattery ) {
            currentBattery += chargeSpeed / Time.deltaTime;

            if ( currentBattery > maxBattery ) {
                currentBattery = maxBattery;
            }
        }
    }

    /// <summary>
    /// Reset component to default
    /// values.
    /// </summary>
    /// <param name="fullReset">bool - whether to reset this component to init game status.</param>
    public void Reset( bool fullReset = false ) {
        currentBattery = maxBattery;
        enabled = false;
        isCharging = false;

        if ( fullReset ) {
            maxBattery = 60f;
            currentBattery = maxBattery;
        }
    }
}
