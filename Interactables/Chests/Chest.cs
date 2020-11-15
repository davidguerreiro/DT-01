using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public bool opened;                                     // Flag to control if this chest has already been opened.
    
    [Header("Data Source")]
    public InteractableData chestData;                      // Data source reference.

    [Header("Components")]
    public Loot loot;                                       // Loot class component reference.
    public Animator anim;                                   // Animator component reference.
    public ParticleSystem particles;                        // Particle system component reference.
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        Debug.Log( "entered" );
        // show interaction notification to player if this chest has not been opened.
        if ( ! opened && other.gameObject.tag == "Player" && GamePlayUI.instance != null ) {
            GamePlayUI.instance.interactNotification.SetUp( chestData.label, chestData.actionSpeed );
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other) {
        // remove interaction notification if displayed.
        if ( ! opened && other.gameObject.tag == "Player" && GamePlayUI.instance != null && GamePlayUI.instance.interactNotification.displayed ) {
            GamePlayUI.instance.interactNotification.Hide();
        }
    }
}
