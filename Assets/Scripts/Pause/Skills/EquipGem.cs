using UnityEngine;
using UnityEngine.UI;

public class EquipGem : MonoBehaviour {

    public ElementsMenu elementsMenu;   // reference to ElementsMenu
    public GemsInventory gemsInventory; // reference to GemsInventory
    public string elementName;  // name of this gem's element

    public Sprite crystalSprite;

    // gem slots to drag to
    public RectTransform slot1;
    public RectTransform slot2;

    // for dragging
    private bool isDragging;
    private Vector2 slotsizeDelta;

    // Use this for initialization
    void Start () {
        isDragging = false;

        slotsizeDelta = slot1.sizeDelta;    // take either slot1 or slot2 sizeDelta
    }
	
	// Update is called once per frame
	void Update () {

	    if (isDragging) {
            gemsInventory.ghostGemSprite.transform.position = Input.mousePosition;
        }
	}

    public void BeginDrag()
    {
        // if player has 0 of this gem, player cannot equip this element
        if (PlayerAction.instance.GetPlayerData().GetCrystalCount(elementName) == 0)
            return;

        gemsInventory.ghostGemSprite.gameObject.SetActive(true);
        gemsInventory.ghostGemSprite.sprite = crystalSprite;

        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDragging)    // not dragging
            return;

        gemsInventory.ghostGemSprite.gameObject.SetActive(false);
        isDragging = false;

        // check if user dragged to any slot successfully
        Vector3 distCheckSlot1 = slot1.position - Input.mousePosition;
        if (Mathf.Abs(distCheckSlot1.x) < slotsizeDelta.x &&
            Mathf.Abs(distCheckSlot1.y) < slotsizeDelta.y)
        {
            // check whether element is already equipped in a slot
            Element ele1 = PlayerAction.instance.GetPlayerData().GetElementOne();
            Element ele2 = PlayerAction.instance.GetPlayerData().GetElementTwo();

            if (ele1 != null)
            {
                if (ele1.name == elementName)   // already assigned; don't need assign again
                    return;
            }
            if (ele2 != null)
            {
                if (ele2.name == elementName)   // already assigned; but in different slot
                {
                    // swap ele2 to ele1 slot; cause player intends to assign THIS to ele1
                    elementsMenu.SwapElements();
                    return;
                }
            }

            elementsMenu.SetElementOne(elementName);
            return;
        }

        Vector3 distCheckSlot2 = slot2.position - Input.mousePosition;
        if (Mathf.Abs(distCheckSlot2.x) < slotsizeDelta.x &&
            Mathf.Abs(distCheckSlot2.y) < slotsizeDelta.y)
        {
            // check whether element is already equipped in a slot
            Element ele1 = PlayerAction.instance.GetPlayerData().GetElementOne();
            Element ele2 = PlayerAction.instance.GetPlayerData().GetElementTwo();

            if (ele1 != null)
            {
                if (ele1.name == elementName)   // already assigned; but in different slot
                {
                    // swap ele1 to ele2 slot; cause player intends to assign THIS to ele2
                    elementsMenu.SwapElements();
                    return;
                }
            }
            if (ele2 != null)
            {
                if (ele2.name == elementName)   // already assigned; don't need assign again
                    return;
            }

            elementsMenu.SetElementTwo(elementName);
            return;
        }
    }

}
