using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : ScriptableObject {
    public int id = 0;                          // Unique id for this skill.
    public string name;                         // Skill name.
    public string es_name;                      // Skill name in Spanish.
    public string description;                  // Skill description. To be displayed in the UI. 
    public string es_description;               // Spanish description.
    public int cost = 0;                        // Unlock skill costs. Number of shards to be payed by the player to unlock the skill.
    public bool unlocked = false;               // True only if the player has unlocked the skill.
    public enum SkillType {
        combat,
        condition,
        survive,
    };

    public SkillType skillType = new SkillType();       // Skill type - defines to which skill three the skills belogns to.
}
