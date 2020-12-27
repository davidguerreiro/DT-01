using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour {
    public static GamePlayUI instance;                      // Class static instance reference - unique class instance.
    public PlayerDamagedComponent playerDamagedComponent;   // Player damage component wrapper for blood damage elemnts in the gameplay UI.
    public PlayerHealComponent playerHealComponent;         // Player health component wrapper to be used when the player recovers health.
    public ShardsSectionUI shardsComponent;                 // Shards section component wrapper to be used when the player collects a shard in real time gameplay.
    public ItemObtainedSection itemObtainedSection;         // Item obtained section component wrapper to be used when the player collects a new item in real time gameplay.
    public BaseHealthBar healthBar;                         // Player health bar.
    public InteractSection interactNotification;            // Interactable notification. Displays actions that the player can perform.
    public EnemyDataSection enemyDataSection;               // Enemy data section component.
    public DamageSection damageSection;                     // Damage section classc component reference.
    public WeaponSectionUI weaponSectionUI;                 // Weapon section UI class component reference.
    public LevelUpSection levelUpSection;                 // Level up section class component refernece.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Display player damaged
    /// animation in the UI.
    /// </summary>
    public void PlayerDamaged() {

        if ( playerDamagedComponent.bloodPanel != null && playerDamagedComponent.bloodImage != null ) {
            playerDamagedComponent.bloodPanel.Display();
            playerDamagedComponent.bloodImage.Display();
        }

        if ( healthBar != null ) {
            healthBar.PlayDamagedAnim();
        }
    }

    /// <summary>
    /// Display player healing
    /// animation in the UI.
    /// </summary>
    public void PlayerHealed() {

        if ( playerHealComponent.healthPanel != null ) {
            // playerHealComponent.healthPanel.Display();
        }

        if ( healthBar != null ) {
            healthBar.PlayHealAnim();
        }
    }
}
