using UnityEngine;
using System.Collections;

public class NPCDialogue : MonoBehaviour {

    public TextAsset dialogueFile;
    public string[] textLines;

	// Use this for initialization
	void Start () {

        if (dialogueFile != null)
        {
            textLines = (dialogueFile.text.Split('\n'));
            // split further by horizontal tabs, '\t'
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
}

public enum DIALOGUE_TYPE
{
    DIALOGUE_INTRO,
    DIALOGUE_RESPONSE,
}

public struct Dialogue
{

}