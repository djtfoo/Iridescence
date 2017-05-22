using UnityEngine;
using System.Collections;

public static class PlayerData {

    // can read from txt file so dunnid recompile. like lua scripting liddat
    // players' skill data need read from file also

    // each weapon and/or attack skill will have its attack range.
    // Weapon objects shld be in PlayerInfo class
    // shld have a "pointer" to the current skill - the one that was clicked to do the attack
    public static float attackRangeSquared = 0.5f;  // temp variable to represent weapon
    public static float attackDmg = 10f;    // temp damage variable

    public static float converseRangeSquared = 0.5f;  // distance between player & NPC to start dialogue
}