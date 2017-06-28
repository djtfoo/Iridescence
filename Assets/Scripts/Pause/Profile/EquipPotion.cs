using UnityEngine;
using UnityEngine.UI;

public class EquipPotion : MonoBehaviour {

    public PotionsInventory potionsInventory;   // reference to potionsInventory
    public string potionName;   // name of this potion

    public Sprite potionSprite; // to replace ghost when dragging starts
    
    // child variables
    public Image childSprite;
    public Text childQuantity;

    // for dragging
    private bool isDragging;

    // for assigning
    private RectTransform[] slotImages;
    private Vector2 slotsizeDelta;

    // Use this for initialization
    void Start()
    {
        isDragging = false;

        slotImages = potionsInventory.slots;
        slotsizeDelta = potionsInventory.slots[0].sizeDelta;    // take any slot sizeDelta
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging) {
            potionsInventory.ghostPotionSprite.transform.position = Input.mousePosition;
        }
    }

    public void BeginDrag()
    {
        potionsInventory.ghostPotionSprite.gameObject.SetActive(true);
        potionsInventory.ghostPotionSprite.sprite = potionSprite;

        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDragging)    // not dragging
            return;

        potionsInventory.ghostPotionSprite.gameObject.SetActive(false);
        isDragging = false;

        // check if user dragged to any slot successfully
        for (int i = 0; i < slotImages.Length; ++i)
        {
            Vector3 distCheckSlot = slotImages[i].position - Input.mousePosition;
            if (Mathf.Abs(distCheckSlot.x) < slotsizeDelta.x &&
                Mathf.Abs(distCheckSlot.y) < slotsizeDelta.y)
            {
                // check whether element is already equipped in a slot
                if (potionName != PlayerAction.instance.GetPlayerData().GetElementOne().name &&
                    potionName != PlayerAction.instance.GetPlayerData().GetElementTwo().name)
                    potionsInventory.potionSlots.SetPotion(potionName, i);
                return;
            }
        }
    }   // end of EndDrag()

}
