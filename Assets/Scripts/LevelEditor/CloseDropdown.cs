using UnityEngine;
using System.Collections;

public class CloseDropdown : MonoBehaviour {

    public Transform content;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Close()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        RectTransform rect = content.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0f, rect.sizeDelta.y);

        // reset tabs to normal
        Transform tab = this.GetComponent<AssetDropList>().GetTab();
        if (tab.GetComponent<LoadResources>().dropdown)
            tab.localPosition = new Vector3(tab.localPosition.x, tab.localPosition.y + 10f, 0f);
        else
            tab.localPosition = new Vector3(tab.localPosition.x, tab.localPosition.y - 10f, 0f);

        this.gameObject.SetActive(false);
    }

}
