using UnityEngine;
using UnityEngine.UI;

public enum TooltipType3D   // denotes whether there is a need for special text
{
    TOOLTIP_NORMAL,
    TOOLTIP_PORTALARROW
}

// A brief description that follows the cursor when mouse is over a GameObject; has background
public class MouseOverTooltip3D : MonoBehaviour {

    public string tooltip;  // string to be output
    public GameObject tooltip3D;    // GameObject containing text to be rendered; is a "static" object

    public TooltipType3D tooltipType;

    private bool isActive;
    private Vector2 tooltipCoordinates;

    private void Start()
    {
        // set reference to 3D tooltip if not done so
        if (tooltip3D == null)
            tooltip3D = GameHUD.instance.tooltip3D;

        // set to inactive first
        tooltip3D.SetActive(false);
        isActive = false;

        tooltipCoordinates = transform.root.localScale.x * tooltip3D.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnMouseOver()
    {
        // set to active to be visible
        tooltip3D.SetActive(true);

        isActive = true;

        // set the text to be the tooltip for this object
        switch (tooltipType)
        {
            case TooltipType3D.TOOLTIP_NORMAL:
                tooltip3D.transform.GetChild(0).GetComponent<Text>().text = tooltip;
                break;
            case TooltipType3D.TOOLTIP_PORTALARROW:
                tooltip3D.transform.GetChild(0).GetComponent<Text>().text = this.transform.parent.GetComponent<Portal>().connectedLocationName;
                break;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            // follow user's cursor
            tooltip3D.transform.position = Input.mousePosition + new Vector3(tooltipCoordinates.x, tooltipCoordinates.y, 0f);
        }
    }

    private void OnMouseExit()
    {
        // set to inactive
        tooltip3D.SetActive(false);

        isActive = false;
    }
}
