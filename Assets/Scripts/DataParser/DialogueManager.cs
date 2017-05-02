using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Render a string onto the dialogue box letter by letter and breaking line
public class DialogueManager : MonoBehaviour {

    // dialogue box prefab
    public Transform dialogueBoxPrefab;
    // dialogue selection prefab
    public Transform selectionPrefab;
    // real-time created dialogue box
    Transform dialogueBox;
    Text NPCName;
    Text message;
    Transform selections;

    // static dialogue box information
    public static bool inDialogue = false;  // whether player is in dialogue or not
    public static DialogueManager dManager; // for player's access

    // private variables to handle running of dialogue output
    //string[] greetings;
    Dialogue currNPCdialogue;
    string lineToOutput;    // current line that is being output
    int dialogueIdx;    // index of current line that is being output

    string toMessageText;   // what is shown in the dialogue box at the moment
    int lineIndex = 0;  // which char of the string the pointer is at
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
            else if (child.name == "Message")
                message = child.GetComponent<Text>();
            else
                selections = child.transform;
        }

        selections.gameObject.SetActive(false);

        NPCName.text = NPC.name;
        currNPCdialogue = NPC.GetComponent<NPCDialogue>().GetDialogue();
        Random.seed = (int)System.DateTime.Now.Ticks;
        int rand = Random.Range(0, currNPCdialogue.greetings.Length);
        lineToOutput = currNPCdialogue.greetings[rand];
        dialogueIdx = -1;

        inDialogue = true;
        closeConvo = false;
        message.text = "";
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
            message.text = toMessageText;
            ++lineIndex;
            outputTimer = 0f;
            if (lineIndex == lineToOutput.Length) {
                lineComplete = true;
                CreateSelections(dialogueIdx);
            }
        }

        if (closeConvo)
        {
            inDialogue = false;
            CloseDialogue();
        }
    }

    private void CreateSelections(int idx)
    {
        if (idx != -1) {
            if (!currNPCdialogue.replies[idx].haveReply)
                return;
        }

        selections.gameObject.SetActive(true);

        // selections need to know the index of each message
        selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, 40f);

        for (int i = 0; i < currNPCdialogue.replies.Length; ++i)
        {
            if (currNPCdialogue.replies[i].dependency == idx) {
                Transform newSelection = (Transform)Instantiate(selectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newSelection.SetParent(selections);
                newSelection.GetChild(0).GetComponent<Text>().text = currNPCdialogue.replies[i].selection;
                selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
                selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y + 10f, selections.localPosition.z);
            }
        }

        if (idx == -1)
        {
            Transform newSelection = (Transform)Instantiate(selectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newSelection.SetParent(selections);
            newSelection.GetChild(0).GetComponent<Text>().text = "Bye";
            selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
            selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y + 10f, selections.localPosition.z);
        }

        if (idx == -1)
        {
            Transform newSelection = (Transform)Instantiate(selectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newSelection.SetParent(selections);
            newSelection.GetChild(0).GetComponent<Text>().text = "Bye2";
            selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
            selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y + 10f, selections.localPosition.z);
        }

        if (idx == -1)
        {
            Transform newSelection = (Transform)Instantiate(selectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newSelection.SetParent(selections);
            newSelection.GetChild(0).GetComponent<Text>().text = "Bye3";
            selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
        }
    }

    public void CloseDialogue()
    {
        // delete the dialogue box prefab
        Destroy(dialogueBox);
    }

}
