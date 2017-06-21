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

    public static float converseRangeSquared = 0.5f;  // distance between player & NPC to start dialogue

    private static float maxHP;
    private static float maxMP;

    private static float HP;
    private static float MP;

    public static void Init()
    {
        maxHP = 100;
        maxMP = 150;

        HP = maxHP;
        MP = maxMP;

    }

    // HP
    public static float GetHP() {
        return HP;
    }
    public static float GetMaxHP() {
        return maxHP;
    }
    public static void TakeDamage(float dmg)
    {
        SetHP(HP - dmg);

        // edit HP bar
        GameHUD.instance.HPChanged(HP, maxHP);
    }
    static void SetHP(float newHP)
    {
        HP = newHP;
        //if (newHP <= 0)
            // lose game
    }
    
    // MP
    public static float GetMP()
    {
        return MP;
    }
    public static float GetMaxMP() {
        return maxMP;
    }
    public static void UseMP(float cost)
    {
        SetMP(MP - cost);

        // edit MP bar
        GameHUD.instance.MPChanged(MP, maxMP);
    }
    static void SetMP(float newMP) {
        MP = newMP;

        if (newMP <= 0)
            MP = 0;
    }

}