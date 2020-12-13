using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSectionUI : MonoBehaviour {
    public PlasmaGun data;                              // Main weapon data source.
    [Header("Experience Text")]
    public ExpText currentExp;                          // Current exp obtained exp text class component reference.
    public ExpText slashText;                           // Slash text exp text component reference.
    public ExpText totalExp;                            // Total exp required to level up class component reference.
    public ExpGot expGot;                               // Exp got from action class component refernece.

    [Header("Munition Circle")]
    public ExpCircleBar expCircleBar;                   // Experience bar class component reference.
    // TODO: Complete munition circle and complete the rest of this class to
    // make munition and exp UI work within the game.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
