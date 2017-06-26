using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

// A brief description that follows the cursor when mouse is over a GameObject; has background
public class MouseOverTooltip3D : MonoBehaviour {

    public string tooltip;  // string to be output
    public GameObject tooltipText;    // GameObject containing text to be rendered; is a "static" object

    private bool isActive;
    private Vector2 tooltipHalfCoordinates;

    private void Start()
    {
        // set to inactive first
        tooltipText.SetActive(false);
        isActive = false;

        tooltipHalfCoordinates = 0.5f * tooltipText.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnMouseOver()
    {
        // set to active to be visible
        tooltipText.gameObject.SetActive(true);

        isActive = true;

        // set the text to be the tooltip for this object
        tooltipText.transform.GetChild(0).GetComponent<Text>().text = tooltip;
    }

    private void Update()
    {
        if (isActive)
        {
            // follow user's cursor
            tooltipText.transform.position = Input.mousePosition + new Vector3(tooltipHalfCoordinates.x, tooltipHalfCoordinates.y, 0f);
        }
    }

    private void OnMouseExit()
    {
        // set to inactive
        tooltipText.SetActive(false);

        isActive = false;
    }
}
