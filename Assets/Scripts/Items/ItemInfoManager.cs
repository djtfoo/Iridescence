using UnityEngine;
using System.Collections.Generic;

// class that contains information of different items
public class ItemInfoManager : MonoBehaviour {

    // Potions
    private TextAsset[] potionsXMLFiles;
    public Dictionary<string, Potion> potionsList;   // deserialize each Potion from XML

    public static ItemInfoManager instance;

	// Use this for initialization
	private void Awake() {
        instance = GetComponent<ItemInfoManager>(); // this

        // load potion data from Resources
        potionsXMLFiles = Resources.LoadAll<TextAsset>("Items/Potions");

        potionsList = new Dictionary<string, Potion>();
        for (int i = 0; i < potionsXMLFiles.Length; ++i)
        {
            // deserialize XML
            Potion tempPotion = XMLSerializer<Potion>.DeserializeXMLFile(potionsXMLFiles[i]);

            tempPotion.Init();

            // add to dictionary
            potionsList.Add(tempPotion.name, tempPotion);
        }
    }

    public Potion GetPotion(string key)
    {
        return potionsList[key];
    }

}
