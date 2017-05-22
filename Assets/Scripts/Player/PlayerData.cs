using UnityEngine;
using System.Collections;

public static class PlayerData {

    // each weapon and/or attack skill will have its attack range.
    // Weapon objects shld be in PlayerInfo class
    // shld have a "pointer" to the current skill - the one that was clicked to do the attack
    public static float attackRangeSquared = 1f;  // temp variable to represent weapon
    public static float attackDmg = 10f;    // temp damage variable

}