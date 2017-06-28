using UnityEngine;
using UnityEngine.UI;

public class PotionsHUD : MonoBehaviour {

    public static PotionsHUD instance;
    public Image[] potionSprites;

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
        }
        else
        {
            Potion potion = ItemInfoManager.instance.GetPotion(potionName); // reference to potion that is being set
            potionSprites[slotIdx].sprite = potion.GetPotionSprite();   // set sprite
            potionSprites[slotIdx].color = new Color(1f, 1f, 1f, 1f);   // visible

            // set to Potions tooltip
            potionSprites[slotIdx].transform.parent.GetComponent<PotionsTooltip>().SetPotion(potion);
        }
    }

}
