using UnityEngine;
using UnityEngine.UI;

public class EquipGem : MonoBehaviour {

    public ElementsMenu elementsMenu;   // reference to ElementsMenu
    public string elementName;  // name of this gem's element

    public Sprite crystalSprite;
    public Image ghostGemSprite;    // sprite that follows cursor

    // gem slots to drag to
    public RectTransform slot1;
    public RectTransform slot2;

    // for dragging
    private bool isDragging;
    private Vector2 slotsizeDelta;

    // Use this for initialization
    void Start () {
        ghostGemSprite.gameObject.SetActive(false);
        isDragging = false;

        slotsizeDelta = slot1.sizeDelta;    // take either slot1 or slot2 sizeDelta
    }
	
	// Update is called once per frame
	void Update () {

	    if (isDragging)
        {
            ghostGemSprite.transform.position = Input.mousePosition;
        }
	}

    public void BeginDrag()
    {
        // if player has 0 of this gem, player cannot equip this element
        if (PlayerAction.instance.GetPlayerData().GetCrystalCount(elementName) == 0)
            return;

        ghostGemSprite.gameObject.SetActive(true);
        ghostGemSprite.sprite = crystalSprite;

        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDragging)    // not dragging
            return;

        ghostGemSprite.gameObject.SetActive(false);
        isDragging = false;

        // check if user dragged to any slot successfully
        Vector3 distCheckSlot1 = slot1.position - Input.mousePosition;
        if (Mathf.Abs(distCheckSlot1.x) < slotsizeDelta.x &&
            Mathf.Abs(distCheckSlot1.y) < slotsizeDelta.y)
        {
            // check whether element is already equipped in a slot
            if (elementName != PlayerAction.instance.GetPlayerData().GetElementOne().name &&
                elementName != PlayerAction.instance.GetPlayerData().GetElementTwo().name)
                elementsMenu.SetElementOne(elementName);
            return;
        }

        Vector3 distCheckSlot2 = slot2.position - Input.mousePosition;
        if (Mathf.Abs(distCheckSlot2.x) < slotsizeDelta.x &&
            Mathf.Abs(distCheckSlot2.y) < slotsizeDelta.y)
        {
            // check whether element is already equipped in a slot
            if (elementName != PlayerAction.instance.GetPlayerData().GetElementOne().name &&
                elementName != PlayerAction.instance.GetPlayerData().GetElementTwo().name)
                elementsMenu.SetElementTwo(elementName);
            return;
        }
    }

}
