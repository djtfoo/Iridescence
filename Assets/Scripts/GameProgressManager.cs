using UnityEngine;
using System.Collections;

// class to store save data that reflects game's progress
public class GameProgressManager : MonoBehaviour {

    Cutscene[] cutscenes;
    QuestManager questManager;

    private void Awake()
    {

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
