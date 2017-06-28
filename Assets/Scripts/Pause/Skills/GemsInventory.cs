using UnityEngine;
using UnityEngine.UI;

public class GemsInventory : MonoBehaviour {

    public Image ghostGemSprite;    // sprite that follows cursor

    // Text references for each gem's quantity
    public Text fireQuantity;
    public Text waterQuantity;
    public Text earthQuantity;
    public Text airQuantity;

    // reference to PlayerData
    private PlayerData playerData;

    // Use this for initialization
    void Start () {
        ghostGemSprite.gameObject.SetActive(false);

        // set reference to PlayerData
        playerData = PlayerAction.instance.GetPlayerData();
    }

    public void Init()
    {
        // set the quantity text
        fireQuantity.text = playerData.GetCrystalCount("Fire").ToString();
        waterQuantity.text = playerData.GetCrystalCount("Water").ToString();
        earthQuantity.text = playerData.GetCrystalCount("Earth").ToString();
        airQuantity.text = playerData.GetCrystalCount("Air").ToString();
    }

}
