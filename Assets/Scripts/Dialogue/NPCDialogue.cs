using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Dialogue
{
    public string firstGreeting;
    public string[] greetings;
    public string goodbye;

    public DialogueReply[] replies;
}

[Serializable]
public class DialogueReply
{
    public string selection;    // what player chooses to start this topic
    public int index;       // index of this message
    public int dependency;  // -1 for greeting, 0 onwards representing the index of the msg it's replying
    public string dialogue; // the NPC's reply
    public bool haveReply;  // this message has at least 1 reply
}

public class NPCDialogue : MonoBehaviour {

    public TextAsset dialogueFile;
    Dialogue thisNPCDialogue;

    private bool firstTimeGreetingPlayer = false;   // whether it's first time player talking to this NPC

    public bool IsFirstTimeGreetingPlayer()
    {
        return firstTimeGreetingPlayer;
    }
    public void SetFirstTimeGreetingPlayer(bool isFirst)
    {
        firstTimeGreetingPlayer = isFirst;
    }

	// Use this for initialization
	void Start () {

        if (dialogueFile != null)
        {
            string dataAsJSON = dialogueFile.text;
            thisNPCDialogue = JsonUtility.FromJson<Dialogue>(dataAsJSON);
        }
	}
	
    public Dialogue GetDialogue()
    {
        return thisNPCDialogue;
    }

	// Update is called once per frame
	void Update () {

	}
}