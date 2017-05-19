using UnityEngine;
using System.Collections;

public class AssetDropList : MonoBehaviour {

    Vector2 rectSize;   // this sizeDelta
    Transform tab;   // button that activated this

	// Use this for initialization
	void Start () {
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width * 0.5f, rect.sizeDelta.y * 0.8f);
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);

        // set content box
        RectTransform content = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(0f, content.sizeDelta.y);

        rectSize = rect.sizeDelta;
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Setters
    public void SetCurrentTab(Transform newTab)
    {
        if (this.gameObject.activeSelf) // already active
        {
            this.GetComponent<CloseDropdown>().Close(); // destroy existing selections
            
            // reset tabs to normal
            //if (button.GetComponent<LoadResources>().dropdown)
            //    button.localPosition = new Vector3(button.localPosition.x, button.localPosition.y + 10f, 0f);
            //else
            //    button.localPosition = new Vector3(button.localPosition.x, button.localPosition.y - 10f, 0f);
        }

        this.gameObject.SetActive(true);

        // shift button downwards & shift list
        if (newTab.GetComponent<LoadResources>().dropdown) {
            newTab.localPosition = new Vector3(newTab.localPosition.x, newTab.localPosition.y - 10f, 0f);
            transform.localPosition = new Vector3(newTab.localPosition.x + 0.4f * rectSize.x, newTab.localPosition.y - 0.7f * rectSize.y, 0f);
        }
        else {
            newTab.localPosition = new Vector3(newTab.localPosition.x, newTab.localPosition.y + 10f, 0f);
            transform.localPosition = new Vector3(newTab.localPosition.x + 0.4f * rectSize.x, newTab.localPosition.y + 0.7f * rectSize.y, 0f);
        }

        tab = newTab;
    }

    // Getters
    public Transform GetTab()
    {
        return tab;
    }
    public float GetSizeDeltaX()
    {
        return rectSize.x;
    }
    public float GetSizeDeltaY()
    {
        return rectSize.y;
    }

}
