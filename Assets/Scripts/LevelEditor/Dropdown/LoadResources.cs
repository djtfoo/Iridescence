using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadResources : MonoBehaviour {

    Transform[] resources;  // to store the level assets from Resources folder
    bool toLoad = true;     // whether resources have been loaded

    public string path; // directory in the Resources folder to load
    public Transform dropdownList;  // list with generated tile selections
    public bool dropdown;   // does the list go upwards or downwards

    public Transform defaultSelectionButton;    // to spawn inside the dropdown list

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    // on button click
    public void ToggleDropdown()
    {
        // it's this tab
        if (dropdownList.gameObject.activeSelf && dropdownList.GetComponent<AssetDropList>().GetTab() == this.transform)
        {
            dropdownList.GetComponent<CloseDropdown>().Close();
        }
        else
        {
            LoadAll();
        }
    }

    void LoadAll()
    {
        if (toLoad) {
            resources = Resources.LoadAll<Transform>(path);
            toLoad = false;
        }

        // set dropdown list to be at player
        dropdownList.GetComponent<AssetDropList>().SetCurrentTab(this.transform);
        // generate each tile as a button to change user's ghost tile
        GenerateResources();
        // set the listener to call function

        // when press button close of program, unload all resources
    }

    // wrapper function to generate resource selection
    void GenerateResources()
    {
        for (int i = 0; i < resources.Length; ++i)
        {
            CreateButton(i);
        }
    }
    
    void CreateButton(int idx)
    {
        Transform newButton = (Transform)Instantiate(defaultSelectionButton, new Vector3(0, 0, 0), Quaternion.identity);
        newButton.GetComponent<Image>().sprite = resources[idx].GetComponent<SpriteRenderer>().sprite;
        newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
        newButton.GetComponent<AssetInfo>().SetAssetInfo(resources[idx].GetComponent<AssetInfo>().GetID(), resources[idx].GetComponent<AssetInfo>().GetGOType());

        RectTransform content = dropdownList.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        newButton.SetParent(content.transform);
        // resize content box
        content.sizeDelta = new Vector2(content.sizeDelta.x + 150f, content.sizeDelta.y);

        newButton.localScale = new Vector3(0.5f, 0.5f, 1);
    }


    //public void UnloadResources()
    //{
    //
    //}

}
