using UnityEngine;
using UnityEngine.UI;

public class PotionsInventory : MonoBehaviour {

    public Transform potionItemPrefab; // a single potion in the inventory
    public Image ghostPotionSprite; // sprite that follows cursor

    public PotionSlots potionSlots; // reference to potion slots

    // Keybind Potion Slots
    public RectTransform[] slots;

    // reference to player data
    private PlayerData playerData;

    private bool needInitInventory = true;  // load potions for viewing menu

    // reference to potion tooltip - for children to access
    public Transform tooltip;
    public Text tooltipName;
    public Text tooltipDescription;
    public Text tooltipDuration;

    private void Start()
    {
        playerData = PlayerAction.instance.GetPlayerData();
        ghostPotionSprite.gameObject.SetActive(false);
    }

    public void Init()
    {
        // add a potion item for each potion in ItemInfoManager
        if (needInitInventory)
        {
            foreach (string key in ItemInfoManager.instance.potionsList.Keys)
            {
                // get reference to potion
                Potion potion = ItemInfoManager.instance.GetPotion(key);

                // instantiate a potion item to add to inventory horizontal layout group
                Transform potionItem = (Transform)Instantiate(potionItemPrefab, Vector3.zero, Quaternion.identity);
                potionItem.SetParent(transform);    // set this PotionInventory as parent

                EquipPotion equipPotionComponent = potionItem.GetComponent<EquipPotion>();

                // set reference to this for PotionInventory
                equipPotionComponent.potionsInventory = this;

                // set potion's name in EquipPotion component
                equipPotionComponent.potionName = key;   // key is the potion's name

                // set potion's sprite in EquipPotion component
                equipPotionComponent.potionSprite = potion.GetPotionSprite();

                // set child potion sprite
                equipPotionComponent.childSprite.sprite = potion.GetPotionSprite();

                //// set child quantity text
                //equipPotionComponent.childQuantity.text = playerData.GetPotionQuantity(key).ToString();

                potionItem.localScale = new Vector3(1f, 1f, 1f);

                // assign PotionTooltip
                PotionsTooltip tooltipComponent = potionItem.GetComponent<PotionsTooltip>();
                tooltipComponent.parentCanvas = transform.root.GetChild(0);
                tooltipComponent.tooltip = tooltip;
                tooltipComponent.potionName = tooltipName;
                tooltipComponent.potionDescription = tooltipDescription;
                tooltipComponent.potionDuration = tooltipDuration;

                tooltipComponent.Init();
                tooltipComponent.SetPotion(potion);
            }

            needInitInventory = false;
        }

        // set quantity
        foreach (Transform child in transform)
        {
            EquipPotion equipPotionComponent = child.GetComponent<EquipPotion>();

            // set child quantity text
            equipPotionComponent.childQuantity.text = playerData.GetPotionQuantity(equipPotionComponent.potionName).ToString();
        }

    }

}
