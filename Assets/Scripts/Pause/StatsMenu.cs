using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour {

    public Text ATKValue;
    public Text DEFValue;
    public Text MAGValue;
    public Text SPDValue;

    public Text levelText;
    public Text EXPValue;
    public Transform EXPBar;

    public Text HPValue;
    public Transform HPBar;
    public Text MPValue;
    public Transform MPBar;

    // set reference to player data
    private PlayerData playerData;

    // Use this for initialization
    public void InitSelf() {
        // set reference to player data
        playerData = PlayerAction.instance.GetPlayerData();
    }

    /// <summary>
    ///  Function to set player data variables to the menu
    /// </summary>
    public void InitStatsMenu()
    {
        // set stats values
        ATKValue.text = playerData.statATK.ToString();
        DEFValue.text = playerData.statDEF.ToString();
        MAGValue.text = playerData.statMAG.ToString();
        SPDValue.text = playerData.statSPD.ToString();

        // set level & EXP
        levelText.text = "Lv " + playerData.playerLevel.ToString();
        SetEXPBar(playerData.GetCurrentEXP(), playerData.GetCurrentEXPTotal());

        // set HP bar & MP bar
        SetHPBar(playerData.GetHP(), playerData.maxHP);
        SetMPBar(playerData.GetMP(), playerData.maxMP);
    }

    public void SetEXPBar(int currEXP, int totalEXP)
    {
        EXPValue.text = currEXP.ToString() + "/" + totalEXP.ToString();
        EXPBar.localScale = new Vector3(currEXP / totalEXP, 1f, 1f);
    }
    public void SetHPBar(int newHP, int maxHP)
    {
        HPValue.text = newHP.ToString() + "/" + maxHP.ToString();
        HPBar.localScale = new Vector3(newHP / maxHP, 1f, 1f);
    }

    public void SetMPBar(int newMP, int maxMP)
    {
        MPValue.text = newMP.ToString() + "/" + maxMP.ToString();
        MPBar.localScale = new Vector3(newMP / maxMP, 1f, 1f);
    }

}
