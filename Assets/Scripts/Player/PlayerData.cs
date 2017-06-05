using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerData {

    // can read from txt file so dunnid recompile. like lua scripting liddat
    // players' skill data need read from file also

    // each weapon and/or attack skill will have its attack range.
    // Weapon objects shld be in PlayerInfo class
    // shld have a "pointer" to the current skill - the one that was clicked to do the attack

    public static float converseRangeSquared = 0.5f;  // distance between player & NPC to start dialogue
}