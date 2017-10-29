using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// class to store save data that reflects game's progress
public class GameProgressManager : MonoBehaviour {

    Cutscene[] cutscenes;
    //QuestManager questManager;

    private string currentLocationName; // name of the location the player is currently at

    private static Dictionary<string, int> crystalCount;

    public static GameProgressManager instance;

    public System.Random rand;

    private void Awake()
    {
        instance = GetComponent<GameProgressManager>();

        rand = new System.Random();
    }

    private void Update()
    {

    }

}

enum CUTSCENE_TYPE
{
    CS_ANIMATED,
    CS_FRAMES
}

struct Cutscene
{
    CUTSCENE_TYPE cutscene_type;
    bool seenBefore;
    // condition
}
