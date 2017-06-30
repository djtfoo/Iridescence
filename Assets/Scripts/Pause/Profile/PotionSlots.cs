using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PotionSlot    // variable content in 1 potion slot
{
    public Image slot_sprite;
    public Text slot_quantity;
    public PotionsTooltip slot_tooltip;
}

public class PotionSlots : MonoBehaviour {

    public PotionSlot[] slots;

    // reference to PlayerData
    private PlayerData playerData;

    // Use this for initialization
    void Start () {
        playerData = PlayerAction.instance.GetPlayerData();
    }

    public void InitPotionSlots()
    {
        for (int i = 0; i < 5; ++i)
        {
            SetPotionSlot(playerData.equippedPotions[i], i);
        }
    }

    /// <summary>
    ///  Function that removes the potion at the slot that user clicks
    /// </summary>
    public void RemovePotion(int slotIdx)
    {
        if (PlayerAction.instance.GetPlayerData().equippedPotions[slotIdx] != null)
            SetPotion("", slotIdx);
    }

    /// <summary>
    ///  Called when a potion assignment or removal was executed successfully
    /// </summary>
    public void SetPotion(string potionName, int idx)
    {
        // set for player data
        playerData.SetPotionToSlot(potionName, idx);

        // set for menu
        SetPotionSlot(potionName, idx);
    }

    /// <summary>
    ///  Function that fills up the respective potion slot INSIDE THE MENU ONLY
    /// </summary>
    private void SetPotionSlot(string potionName, int slotIdx)
    {
        if (potionName == "")
        {
            // set empty sprite
            slots[slotIdx].slot_sprite.sprite = null;
            slots[slotIdx].slot_sprite.color = new Color(1f, 1f, 1f, 0f);

            // remove quantity slot
            slots[slotIdx].slot_quantity.text = "";

            // set tooltip
            slots[slotIdx].slot_tooltip.SetPotion(null);
        }
        else
        {
            // get potion
            Potion potion = ItemInfoManager.instance.GetPotion(potionName);

            // set sprite
            slots[slotIdx].slot_sprite.sprite = potion.GetPotionSprite();
            slots[slotIdx].slot_sprite.color = new Color(1f, 1f, 1f, 1f);

            // set quantity
            slots[slotIdx].slot_quantity.text = playerData.GetPotionQuantity(potionName).ToString();

            // set tooltip
            slots[slotIdx].slot_tooltip.SetPotion(potion);
        }
    }

}
