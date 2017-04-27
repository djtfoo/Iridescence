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
    public int dependency;  // -1 for greeting, 0 onwards for response to a reply
    public string dialogue; // the NPC's reply
}

public class NPCDialogue : MonoBehaviour {

    public TextAsset dialogueFile;
    Dialogue thisNPCDialogue;

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