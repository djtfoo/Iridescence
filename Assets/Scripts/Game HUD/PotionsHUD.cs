using UnityEngine;
using UnityEngine.UI;

public class PotionsHUD : MonoBehaviour {

    public static PotionsHUD instance;
    public Image[] potionSprites;
    public Text[] potionQuantityText;

    // Use this for initialization
    private void Awake()
    {
        instance = GetComponent<PotionsHUD>();
    }

    void Start () {
	    // set potions to GameHUD's potion slots
        for (int i = 0; i < potionSprites.Length; ++i)
        {
            SetPotion(PlayerAction.instance.GetPlayerData().equippedPotions[i], i);
        }
	}
	
	public void SetPotion(string potionName, int slotIdx)
    {
        if (potionName == "")
        {
            potionSprites[slotIdx].sprite = null;   // no sprite
            potionSprites[slotIdx].color = new Color(1f, 1f, 1f, 0f);   // no alpha; not visible

            // set null to Potions tooltip
            potionSprites[slotIdx].transform.parent.GetComponent<PotionsTooltip>().SetPotion(null);

            // set quantity text to be empty
            potionQuantityText[slotIdx].text = "";  // empty
        }
        else
        {
            Potion potion = ItemInfoManager.instance.GetPotion(potionName); // reference to potion that is being set
            potionSprites[slotIdx].sprite = potion.GetPotionSprite();   // set sprite
            potionSprites[slotIdx].color = new Color(1f, 1f, 1f, 1f);   // visible

            // set to Potions tooltip
            potionSprites[slotIdx].transform.parent.GetComponent<PotionsTooltip>().SetPotion(potion);

            // set quantity text to be empty
            SetPotionQuantity(PlayerAction.instance.GetPlayerData().GetPotionQuantity(potionName), slotIdx);
        }
    }

    public void SetPotionQuantity(int quantity, int slotIdx)
    {
        potionQuantityText[slotIdx].text = quantity.ToString();

        if (quantity == 0)
            potionSprites[slotIdx].color = new Color(1f, 1f, 1f, 0.5f); // faded out; suggests potions have ran out
        else
            potionSprites[slotIdx].color = new Color(1f, 1f, 1f, 1f);   // fully visible
    }

    // use potion on-click
    public void UsePotion(int slotIdx)
    {
        PlayerAction.instance.UsePotion(slotIdx);
    }

}
