using UnityEngine;
using UnityEngine.UI;

public class SecondaryStatsMenu : MonoBehaviour {

    public Text critChanceValue;
    public Text critChanceOriginal;
    public Text critMultiplier;
    public Text critMultiplierOriginal;

    // set reference to player data
    private PlayerData playerData;

    // Use this for initialization
    void Start () {
        playerData = PlayerAction.instance.GetPlayerData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init()
    {
        /// Critical Hit Chance
        critChanceValue.text = playerData.GetModifiedCritChance().ToString("n1") + "%";
        if (playerData.GetModifiedCritChance() > playerData.criticalHitChance)
        {
            critChanceValue.color = Color.green;
            critChanceOriginal.text = "[" + playerData.criticalHitChance + "]";
        }
        else if (playerData.GetModifiedCritChance() < playerData.criticalHitChance)
        {
            critChanceValue.color = Color.red;
            critChanceOriginal.text = "[" + playerData.criticalHitChance + "]";
        }
        else
        {
            critChanceValue.color = Color.black;
            critChanceOriginal.text = "";
        }

        /// Critical Hit Multiplier
        critMultiplier.text = playerData.GetModifiedCritMultiplier().ToString("n1");
        if (playerData.GetModifiedCritMultiplier() > playerData.criticalHitMultiplier)
        {
            critMultiplier.color = Color.green;
            critMultiplierOriginal.text = "[" + playerData.criticalHitMultiplier + "]";
        }
        else if (playerData.GetModifiedCritMultiplier() < playerData.criticalHitMultiplier)
        {
            critMultiplier.color = Color.red;
            critMultiplierOriginal.text = "[" + playerData.criticalHitMultiplier + "]";
        }
        else
        {
            critMultiplier.color = Color.black;
            critMultiplierOriginal.text = "";
        }

    }

}
