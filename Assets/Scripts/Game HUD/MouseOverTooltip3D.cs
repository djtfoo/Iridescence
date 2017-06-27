using UnityEngine;
using UnityEngine.UI;

// A brief description that follows the cursor when mouse is over a GameObject; has background
public class MouseOverTooltip3D : MonoBehaviour {

    public string tooltip;  // string to be output
    public GameObject tooltipText;    // GameObject containing text to be rendered; is a "static" object

    private bool isActive;
    private Vector2 tooltipCoordinates;

    private void Start()
    {
        // set to inactive first
        tooltipText.SetActive(false);
        isActive = false;

        tooltipCoordinates = transform.root.localScale.x * tooltipText.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnMouseOver()
    {
        // set to active to be visible
        tooltipText.SetActive(true);

        isActive = true;

        // set the text to be the tooltip for this object
        tooltipText.transform.GetChild(0).GetComponent<Text>().text = tooltip;
    }

    private void Update()
    {
        if (isActive)
        {
            // follow user's cursor
            tooltipText.transform.position = Input.mousePosition + new Vector3(tooltipCoordinates.x, tooltipCoordinates.y, 0f);
        }
    }

    private void OnMouseExit()
    {
        // set to inactive
        tooltipText.SetActive(false);

        isActive = false;
    }
}
