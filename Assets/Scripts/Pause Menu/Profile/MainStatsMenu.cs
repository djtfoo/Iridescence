using UnityEngine;
using UnityEngine.UI;

// inits primary stats, and acts as parent of secondary stats
public class MainStatsMenu : MonoBehaviour {

    public SecondaryStatsMenu secondaryStatsMenu;

    // current values - will change color
    public Text ATKValue;
    public Text DEFValue;
    public Text MAGValue;
    public Text SPDValue;
    // original values
    public Text ATKOriginalValue;
    public Text DEFOriginalValue;
    public Text MAGOriginalValue;
    public Text SPDOriginalValue;

    public Text levelText;
    public Text EXPValue;
    public Transform EXPBar;

    public Text HPValue;
    public Transform HPBar;
    public Text MPValue;
    public Transform MPBar;

    public PotionSlots potionSlots;
    public PotionsInventory potionsInventory;

    // set reference to player data
    private PlayerData playerData;

    // Use this for initialization
    private void Start() {
        // set reference to player data
        playerData = PlayerAction.instance.GetPlayerData();
    }

    /// <summary>
    ///  Function to set player data variables to the menu
    /// </summary>
    public void InitSelf()
    {
        //==================
        // Set stats values
        //==================
        /// ATK
        ATKValue.text = ((int)playerData.GetModifiedATK()).ToString();
        if (playerData.GetModifiedATK() > playerData.statATK)
        {
            ATKValue.color = Color.green;
            ATKOriginalValue.text = "[" + playerData.statATK + "]";
        }
        else if (playerData.GetModifiedATK() < playerData.statATK)
        {
            ATKValue.color = Color.red;
            ATKOriginalValue.text = "[" + playerData.statATK + "]";
        }
        else
        {
            ATKValue.color = Color.black;
            ATKOriginalValue.text = "";
        }

        /// DEF
        DEFValue.text = ((int)playerData.GetModifiedDEF()).ToString();
        if (playerData.GetModifiedDEF() > playerData.statDEF)
        {
            DEFValue.color = Color.green;
            DEFOriginalValue.text = "[" + playerData.statDEF + "]";
        }
        else if (playerData.GetModifiedDEF() < playerData.statDEF)
        {
            DEFValue.color = Color.red;
            DEFOriginalValue.text = "[" + playerData.statDEF + "]";
        }
        else
        {
            DEFValue.color = Color.black;
            DEFOriginalValue.text = "";
        }

        /// MAG
        MAGValue.text = ((int)playerData.GetModifiedMAG()).ToString();
        if (playerData.GetModifiedMAG() > playerData.statMAG)
        {
            MAGValue.color = Color.green;
            MAGOriginalValue.text = "[" + playerData.statMAG + "]";
        }
        else if (playerData.GetModifiedMAG() < playerData.statMAG)
        {
            MAGValue.color = Color.red;
            MAGOriginalValue.text = "[" + playerData.statMAG + "]";
        }
        else
        {
            MAGValue.color = Color.black;
            MAGOriginalValue.text = "";
        }

        /// SPD
        SPDValue.text = ((int)playerData.GetModifiedSPD()).ToString();
        if (playerData.GetModifiedSPD() > playerData.statSPD)
        {
            SPDValue.color = Color.green;
            SPDOriginalValue.text = "[" + playerData.statSPD + "]";
        }
        else if (playerData.GetModifiedSPD() < playerData.statSPD)
        {
            SPDValue.color = Color.red;
            SPDOriginalValue.text = "[" + playerData.statSPD + "]";
        }
        else
        {
            SPDValue.color = Color.black;
            SPDOriginalValue.text = "";
        }

        // set level & EXP
        levelText.text = "Lv " + playerData.playerLevel.ToString();
        SetEXPBar(playerData.GetCurrentEXP(), playerData.GetCurrentEXPTotal());

        // set HP bar & MP bar
        SetHPBar(playerData.GetHP(), playerData.maxHP);
        SetMPBar(playerData.GetMP(), playerData.maxMP);

        //===================
        // Init Potions Menu
        //===================
        potionSlots.InitPotionSlots();
        potionsInventory.Init();

        //======================
        // Secondary Stats Menu
        //======================
        secondaryStatsMenu.Init();
    }

    public void SetEXPBar(int currEXP, int totalEXP)
    {
        EXPValue.text = currEXP + "/" + totalEXP;
        EXPBar.localScale = new Vector3((float)currEXP / totalEXP, 1f, 1f);
    }
    public void SetHPBar(float newHP, float maxHP)
    {
        HPValue.text = (int)newHP + "/" + maxHP;
        HPBar.localScale = new Vector3(newHP / maxHP, 1f, 1f);
    }

    public void SetMPBar(float newMP, float maxMP)
    {
        MPValue.text = (int)newMP + "/" + maxMP;
        MPBar.localScale = new Vector3(newMP / maxMP, 1f, 1f);
    }

}
