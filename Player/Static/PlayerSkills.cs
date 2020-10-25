using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {
    public static PlayerSkills instance;                                    // Publis static class instance.
    public List<PlayerSkill> combatSkills = new List<PlayerSkill>();        // Player combat skills. This is the static reference used by all the other components.
    public List<PlayerSkill> conditionSkills = new List<PlayerSkill>();     // Player condition skills. This is the static reference used by all the other components.
    public List<PlayerSkill> survivingSkills = new List<PlayerSkill>();     // Player surviving skills. This is the static reference used by all the other components.

    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

}
