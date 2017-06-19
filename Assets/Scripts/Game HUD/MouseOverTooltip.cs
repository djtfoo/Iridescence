using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

// A brief description that follows the cursor when mouse is over a UI element
public class MouseOverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string tooltip;  // string to be output
    public Text tooltipText;    // text to be rendered; is a "static" object

    private bool isActive;
    private Vector2 tooltipHalfCoordinates;

    private void Start()
    {
        // set to inactive first
        tooltipText.gameObject.SetActive(false);
        isActive = false;

        tooltipHalfCoordinates = 0.5f * tooltipText.GetComponent<RectTransform>().sizeDelta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // set to active to be visible
        tooltipText.gameObject.SetActive(true);

        isActive = true;

        // set the text to be the tooltip for this object
        tooltipText.text = tooltip;
    }

    private void Update()
    {
        if (isActive) {
            // follow user's cursor
            tooltipText.transform.position = Input.mousePosition + new Vector3(tooltipHalfCoordinates.x, tooltipHalfCoordinates.y, 0f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // set to inactive
        tooltipText.gameObject.SetActive(false);

        isActive = false;
    }
}
