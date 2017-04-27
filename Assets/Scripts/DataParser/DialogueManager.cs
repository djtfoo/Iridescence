using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Render a string onto the dialogue box letter by letter and breaking line
public class DialogueManager : MonoBehaviour {

    // dialogue box prefab
    public Transform dialogueBoxPrefab;
    // real-time created dialogue box
    Transform dialogueBox;
    Text NPCName;
    Text Message;

    // static dialogue box information
    public static bool inDialogue = false;  // whether player is in dialogue or not
    public static DialogueManager dManager; // for player's access

    // private variables to handle running of dialogue output
    string[] greetings;
    string lineToOutput;
    string toMessageText;
    int lineIndex = 0;
    float outputTimer = 0f;
    float outputBuffer = 0.05f;
    bool lineComplete = false;
    bool closeConvo = false;

    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
    }

    public void InitDialogue(GameObject NPC)
    {
        // create the prefab
        // get the NPC name

        dialogueBox = (Transform)Instantiate(dialogueBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        dialogueBox.SetParent(GameObject.Find("Canvas").transform);
        dialogueBox.localPosition = dialogueBoxPrefab.localPosition;

        foreach (Transform child in dialogueBox.transform)
        {
            if (child.name == "NPC Name")
                NPCName = child.GetComponent<Text>();
            else
                Message = child.GetComponent<Text>();
        }

        NPCName.text = NPC.name;
        greetings = NPC.GetComponent<NPCDialogue>().GetDialogue().greetings;
        Random.seed = (int)System.DateTime.Now.Ticks;
        int rand = Random.Range(0, greetings.Length);
        lineToOutput = greetings[rand];
        inDialogue = true;
        closeConvo = false;
        Message.text = "";
        toMessageText = "";
        lineComplete = false;
    }

    public void RunDialogue(Dialogue NPCDialogue)
    {
        // hold to wait for player to choose response/press "space" to continue
        if (!lineComplete)
        {
            if (!Input.GetKey(KeyCode.Space) && outputTimer < outputBuffer) {
                outputTimer += Time.deltaTime;
                return;
            }
            toMessageText += lineToOutput[lineIndex].ToString();
            Message.text = toMessageText;
            ++lineIndex;
            outputTimer = 0f;
            if (lineIndex == lineToOutput.Length)
                lineComplete = true;
        }

        if (closeConvo)
        {
            inDialogue = false;
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        // delete the dialogue box prefab
        Destroy(dialogueBox);
    }

}
