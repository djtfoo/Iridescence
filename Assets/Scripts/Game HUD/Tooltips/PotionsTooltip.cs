using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionsTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Transform tooltip;

    [Tooltip("Parent canvas for getting change in canvas scale")]
    public Transform parentCanvas;

    public Text potionName;
    public Text potionDescription;
    public Text potionDuration; // in seconds

    private Potion potion;  // reference to the potion this tooltip shows information of

    private bool isActive;
    private Vector2 tooltipCoordinates;

    public bool hasMaxPoint;
    private Vector2 maxPoint;   // maximum that the tooltip box can move to

    public void SetPotion(Potion potion)
    {
        this.potion = potion;
    }

    // Use this for initialization
    void Start() {

        if (parentCanvas == null)
            parentCanvas = transform.root;

        // set to inactive first
        tooltip.gameObject.SetActive(false);
        isActive = false;

        Vector2 sizeDelta = tooltip.GetComponent<RectTransform>().sizeDelta;

        tooltipCoordinates = parentCanvas.localScale.x * 0.5f * sizeDelta;

        if (hasMaxPoint)
            maxPoint = tooltip.localPosition;
        //maxPoint = new Vector2(0.5f * sizeDelta.x, 0.25f * sizeDelta.y);
    }

    // Update is called once per frame
    void Update() {

        if (isActive)
        {
            tooltip.transform.position = Input.mousePosition + new Vector3(tooltipCoordinates.x, tooltipCoordinates.y, 0f);

            if (hasMaxPoint)
                tooltip.transform.localPosition = new Vector3(Mathf.Min(maxPoint.x, tooltip.transform.localPosition.x), Mathf.Max(maxPoint.y, tooltip.transform.localPosition.y),
                    tooltip.transform.localPosition.z);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (potion == null)
            return;

        // set to active to be visible
        tooltip.gameObject.SetActive(true);

        isActive = true;

        // set the text to be the tooltip for this object
        potionName.text = potion.name;
        potionDescription.text = potion.description;
        potionDuration.text = potion.duration.ToString() + " seconds";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // set to inactive
        tooltip.gameObject.SetActive(false);

        isActive = false;
    }

}
