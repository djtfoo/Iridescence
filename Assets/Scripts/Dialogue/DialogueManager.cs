using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Render a string onto the dialogue box letter by letter and breaking line
public class DialogueManager : MonoBehaviour {

    // dialogue box prefab
    public Transform dialogueBoxPrefab;
    // dialogue selection prefab
    public Transform selectionPrefab;
    public Transform closeSelectionPrefab;

    // real-time created dialogue box
    Transform dialogueBox;
    GameObject NPC;
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
    float outputBuffer = 0.01f;
    bool lineComplete = false;
    bool closeConvo = false;

    private bool firstClick = true; // starting click that started dialogue; will trigger skipping of text

    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;

        dManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void Update()
    {
        if (inDialogue)
        {
            dManager.RunDialogue();
            //DialogueManager.dManager.RunDialogue(RaycastInfo.clickTarget.GetComponent<NPCDialogue>().GetDialogue());
        }
    }

    //===================================
    // Initialise & Run
    //===================================
    public void InitDialogue(GameObject npc)
    {
        // create the prefab

        dialogueBox = (Transform)Instantiate(dialogueBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        dialogueBox.SetParent(GameHUD.instance.transform);
        dialogueBox.localPosition = dialogueBoxPrefab.localPosition;

        // get references to GameObjects
        foreach (Transform child in dialogueBox.transform)
        {
            if (child.name == "NPC Name")
                NPCName = child.GetComponent<Text>();   // get the Text object for NPC name
            else if (child.name == "Message")
                message = child.GetComponent<Text>();   // get the Text object for message
            else
                selections = child.transform;
        }

        inDialogue = true;
        closeConvo = false;

        selections.gameObject.SetActive(false);

        // set NPC and current dialogue
        this.NPC = npc;
        NPCName.text = npc.name;
        currNPCdialogue = npc.GetComponent<NPCDialogue>().GetDialogue();

        SetNextMessage(-1);

        firstClick = true;

        //dialogueIdx = -1;
        //Random.seed = (int)System.DateTime.Now.Ticks;
        //int rand = Random.Range(0, currNPCdialogue.greetings.Length);
        //lineToOutput = currNPCdialogue.greetings[rand];
        //
        //message.text = "";
        //toMessageText = "";
        //lineComplete = false;
        //lineIndex = 0;
    }

    public void RunDialogue(/*Dialogue NPCDialogue*/)
    {
        // hold to wait for player to choose response/press "space" to continue
        if (!lineComplete)
        {
            if (firstClick)
            {
                firstClick = false;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                toMessageText = lineToOutput;
                lineIndex = lineToOutput.Length - 1;
            }
            else if (outputTimer < outputBuffer)
            {
                outputTimer += Time.deltaTime;
                return;
            }
            else
            {
                toMessageText += lineToOutput[lineIndex].ToString();
            }
            message.text = toMessageText;
            ++lineIndex;
            outputTimer = 0f;

            if (lineIndex == lineToOutput.Length)
            {
                lineComplete = true;
                CreateSelections(dialogueIdx);
            }
        }

        else if (closeConvo)
        {
            inDialogue = false;
            CloseDialogue();
        }
    }


    //===================================
    // called by Dialogue Option Buttons
    //===================================
    public void ClearSelections()
    {
        // clear off selections
        selections.gameObject.SetActive(false);
        foreach (Transform child in selections)
        {
            Destroy(child.gameObject);
            selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y - 20f);
            selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y - 10f, selections.localPosition.z);
        }
    }
        
    public void SetNextMessage(int idx)
    {
        // set dialogue box's message
        if (idx == -1) {
            if (NPC.GetComponent<NPCDialogue>().IsFirstTimeGreetingPlayer())
            {
                lineToOutput = currNPCdialogue.firstGreeting;
                NPC.GetComponent<NPCDialogue>().SetFirstTimeGreetingPlayer(false);
            }
            int rand = Random.Range(0, currNPCdialogue.greetings.Length);
            lineToOutput = currNPCdialogue.greetings[rand];
        }
        else if (idx == -2)
        {
            lineToOutput = currNPCdialogue.goodbye;
        }
        else {
            lineToOutput = currNPCdialogue.replies[idx].dialogue;
        }

        // reset variables
        dialogueIdx = idx;

        message.text = "";
        toMessageText = "";
        lineComplete = false;
        lineIndex = 0;
    }

    public void SetCloseDialogue()
    {
        closeConvo = true;
    }


    //===================================
    // Wrapper functions
    //===================================
    private void CreateSelections(int idx)
    {
        selections.gameObject.SetActive(true);

        if (idx == -2)
        {
            // create <Close> button
            Transform newSelection = (Transform)Instantiate(closeSelectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newSelection.SetParent(selections);
            selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
            selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y + 10f, selections.localPosition.z);

            return;
        }
        else if (idx != -1) {
            if (!currNPCdialogue.replies[idx].haveReply)
            {
                // create <Continue> button
                Transform newSelection = (Transform)Instantiate(selectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newSelection.SetParent(selections);
                newSelection.GetChild(0).GetComponent<Text>().text = "<Continue>";
                selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
                selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y + 10f, selections.localPosition.z);

                newSelection.GetComponent<DialogueOption>().SetIndex(-1);   // greetings messages are -1

                return;
            }
        }

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

                newSelection.GetComponent<DialogueOption>().SetIndex(currNPCdialogue.replies[i].index);
            }
        }

        if (idx == -1)
        {
            Transform newSelection = (Transform)Instantiate(selectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newSelection.SetParent(selections);
            newSelection.GetChild(0).GetComponent<Text>().text = "Bye";
            selections.GetComponent<RectTransform>().sizeDelta = new Vector2(selections.GetComponent<RectTransform>().sizeDelta.x, selections.GetComponent<RectTransform>().sizeDelta.y + 20f);
            selections.localPosition = new Vector3(selections.localPosition.x, selections.localPosition.y + 10f, selections.localPosition.z);

            newSelection.GetComponent<DialogueOption>().SetIndex(-2);   // goodbye message is -2
        }
    }

    private void CloseDialogue()
    {
        // delete the dialogue box prefab
        Destroy(dialogueBox.gameObject);

        // reset all highlight matters
        NPC.GetComponent<NPCEventHandler>().EndDialogue();
    }

}
