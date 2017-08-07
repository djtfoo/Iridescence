using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveFileMenuData
{
    public Button saveFileButton;   // click to enter the game

    public GameObject textEmpty;    // empty save file
    public GameObject saveFileContent1; // save file's info eg. name, level, gems

    public Text saveName;
    public Text playerLevel;
    public Text fireQuantity;
    public Text waterQuantity;
    public Text earthQuantity;
    public Text airQuantity;
}

public class LoadSaveFiles : MonoBehaviour {

    // for save files
    public SaveFileMenuData[] saveFiles;

    private PlayerData playerData1;
    private PlayerData playerData2;
    private PlayerData playerData3;

    // Use this for initialization
    void Start () {

        // Check Save File 1
        /// get save data XML file
        TextAsset savedataXML = Resources.Load<TextAsset>("SaveData/playerdata1");

        /// deserialize XML file
        if (savedataXML)
        {
            playerData1 = XMLSerializer<PlayerData>.DeserializeXMLFile(savedataXML);
            playerData1.Init();

            saveFiles[0].textEmpty.SetActive(false);
            saveFiles[0].saveFileContent1.SetActive(true);

            /// set save file content
            saveFiles[0].saveName.text = playerData1.playerName;
            saveFiles[0].playerLevel.text = "Lv " + playerData1.playerLevel.ToString();
            saveFiles[0].fireQuantity.text = playerData1.GetCrystalCount("Fire").ToString();
            saveFiles[0].waterQuantity.text = playerData1.GetCrystalCount("Water").ToString();
            saveFiles[0].earthQuantity.text = playerData1.GetCrystalCount("Earth").ToString();
            saveFiles[0].airQuantity.text = playerData1.GetCrystalCount("Air").ToString();
        }
        else
        {
            /// keep it as <EMPTY>
            saveFiles[0].textEmpty.SetActive(true);
            saveFiles[0].saveFileContent1.SetActive(false);

            saveFiles[0].saveFileButton.interactable = false;
        }


        // Check Save File 2
        /// get save data XML file
        TextAsset savedataXML2 = Resources.Load<TextAsset>("SaveData/playerdata2");

        /// deserialize XML file
        if (savedataXML2)
        {
            playerData2 = XMLSerializer<PlayerData>.DeserializeXMLFile(savedataXML2);
            playerData2.Init();

            saveFiles[1].textEmpty.SetActive(false);
            saveFiles[1].saveFileContent1.SetActive(true);

            /// set save file content
            saveFiles[1].saveName.text = playerData2.playerName;
            saveFiles[1].playerLevel.text = "Lv " + playerData2.playerLevel.ToString();
            saveFiles[1].fireQuantity.text = playerData2.GetCrystalCount("Fire").ToString();
            saveFiles[1].waterQuantity.text = playerData2.GetCrystalCount("Water").ToString();
            saveFiles[1].earthQuantity.text = playerData2.GetCrystalCount("Earth").ToString();
            saveFiles[1].airQuantity.text = playerData2.GetCrystalCount("Air").ToString();
        }
        else
        {
            /// keep it as <EMPTY>
            saveFiles[1].textEmpty.SetActive(true);
            saveFiles[1].saveFileContent1.SetActive(false);

            saveFiles[1].saveFileButton.interactable = false;
        }

        // Check Save File 3
        /// get save data XML file
        TextAsset savedataXML3 = Resources.Load<TextAsset>("SaveData/playerdata3");

        /// deserialize XML file
        if (savedataXML3)
        {
            playerData3 = XMLSerializer<PlayerData>.DeserializeXMLFile(savedataXML3);
            playerData3.Init();

            saveFiles[2].textEmpty.SetActive(false);
            saveFiles[2].saveFileContent1.SetActive(true);

            /// set save file content
            saveFiles[2].saveName.text = playerData3.playerName;
            saveFiles[2].playerLevel.text = "Lv " + playerData3.playerLevel.ToString();
            saveFiles[2].fireQuantity.text = playerData3.GetCrystalCount("Fire").ToString();
            saveFiles[2].waterQuantity.text = playerData3.GetCrystalCount("Water").ToString();
            saveFiles[2].earthQuantity.text = playerData3.GetCrystalCount("Earth").ToString();
            saveFiles[2].airQuantity.text = playerData3.GetCrystalCount("Air").ToString();
        }
        else
        {
            /// keep it as <EMPTY>
            saveFiles[2].textEmpty.SetActive(true);
            saveFiles[2].saveFileContent1.SetActive(false);

            saveFiles[2].saveFileButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
