using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TooltipType     // denotes whether there is a need for special text
{
    TOOLTIP_NORMAL,
    TOOLTIP_HP,
    TOOLTIP_MP
}

// A brief description that follows the cursor when mouse is over a UI element
public class MouseOverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string tooltip;  // string to be output
    public Text tooltipText;    // text to be rendered; is a "static" object

    // unique tooltips
    public TooltipType tooltipType;
    //public bool isHP;
    //public bool isMP;

    private bool isActive;
    private Vector2 tooltipHalfCoordinates;

    public bool hasBackground;  // background for text
    // reference to background
    private Transform background;

    // reference to player data
    private PlayerData playerData;

    private void Start()
    {
        // set reference to background
        if (hasBackground)
            background = tooltipText.transform.parent;

        tooltipHalfCoordinates = transform.root.localScale.x * 0.5f * tooltipText.GetComponent<RectTransform>().sizeDelta;

        // set reference to player data
        playerData = PlayerAction.instance.GetPlayerData();

        // set to inactive
        if (hasBackground)
            background.gameObject.SetActive(false);
        else
            tooltipText.gameObject.SetActive(false);
        isActive = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // set to active to be visible
        if (hasBackground)
            background.gameObject.SetActive(true);
        else
            tooltipText.gameObject.SetActive(true);

        isActive = true;

        // set the text to be the tooltip for this object
        tooltipText.text = tooltip;
    }

    private void Update()
    {
        if (isActive) {
            // follow user's cursor
            if (hasBackground)
                background.position = Input.mousePosition + new Vector3(tooltipHalfCoordinates.x, tooltipHalfCoordinates.y, 0f);
            else
                tooltipText.transform.position = Input.mousePosition + new Vector3(tooltipHalfCoordinates.x, tooltipHalfCoordinates.y, 0f);

            switch (tooltipType)
            {
                case TooltipType.TOOLTIP_HP:
                    tooltipText.text = playerData.GetHP().ToString() + " / " + playerData.maxHP;
                    break;
                case TooltipType.TOOLTIP_MP:
                    tooltipText.text = playerData.GetMP().ToString() + " / " + playerData.maxMP;
                    break;

                case TooltipType.TOOLTIP_NORMAL:
                default:
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // set to inactive
        if (hasBackground)
            background.gameObject.SetActive(false);
        else
            tooltipText.gameObject.SetActive(false);

        isActive = false;
    }
}
