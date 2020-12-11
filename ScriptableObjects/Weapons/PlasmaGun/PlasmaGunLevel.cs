using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGunLevel : WeaponLevel {
    [Header("Plasma Gun")]
    public float chargedShootBoost;                         // Charged shoot boost when this level is reached.
    public float meleeDamage;                               // Melee damage shoot bost when this level is reached.
    public int shootCost;                                   // Shoot cost reduction when this level is reached.
    public int chargedShootCost;                            // Plasma charged shoot cost reduction when this level is reached.
    public float rechargedSpeed;                            // Recharged speed boost when this level is reached.
    public float heatedRechargedSpeed;                      // Heated recharge speed boos when this level is reached.
    public int heatedRechargedThreeshold;                   // Heated recharged threeshold reduction when this level is reached.
}
