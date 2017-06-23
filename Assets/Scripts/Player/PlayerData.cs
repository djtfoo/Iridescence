using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// stats
public static class PlayerData {

    // can read from txt file so dunnid recompile. like lua scripting liddat
    // players' skill data need read from file also

    // each weapon and/or attack skill will have its attack range.
    // Weapon objects shld be in PlayerInfo class
    // shld have a "pointer" to the current skill - the one that was clicked to do the attack

    public static float converseRangeSquared = 0.5f;  // distance between player & NPC/Waypoint to start dialogue

    private static float maxHP;
    private static float maxMP;

    private static float HP;
    private static float MP;

    // Player Items
    private static Potion[] potions;
    private static int blankShardsCount;    // number of blank shards player has

    public static void Init()
    {
        maxHP = 100;
        maxMP = 150;

        HP = maxHP;
        MP = maxMP;

    }

    // HP
    public static float GetHP() {   // curr HP
        return HP;
    }
    public static float GetMaxHP() {
        return maxHP;
    }
    public static void TakeDamage(float dmg)
    {
        SetHP(HP - dmg);
    }
    public static void RestoreHP() {
        SetHP(maxHP);
    }
    private static void SetHP(float newHP)
    {
        HP = newHP;
        //if (newHP <= 0)
            // lose game

        // edit HP bar
        GameHUD.instance.HPChanged(HP, maxHP);
    }
    
    // MP
    public static float GetMP() {   // curr MP
        return MP;
    }
    public static float GetMaxMP() {
        return maxMP;
    }
    public static void UseMP(float cost)
    {
        SetMP(MP - cost);
    }
    public static void RestoreMP() {
        SetMP(maxMP);
    }
    private static void SetMP(float newMP) {
        MP = newMP;

        if (newMP <= 0)
            MP = 0;

        // edit MP bar
        GameHUD.instance.MPChanged(MP, maxMP);
    }

}