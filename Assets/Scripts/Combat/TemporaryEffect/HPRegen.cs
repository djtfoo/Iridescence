using UnityEngine;
using UnityEngine.UI;

public class HPRegen : MonoBehaviour {

    public bool infiniteDuration = false;   // whether this buff will go off or not
    public float duration;  // how long this buff will last
    private float timer = 0f;   // timer to count up to the duration -- count up so it's easier for countdown overlay!

    [Tooltip("Amount of HP to regain per second")]
    public float HPPerSecond;   // amount to regain per second

    // reference to PlayerData
    private PlayerData playerData;

    // ModifierHUD
    private Image modifierSprite;   // to add to HUD
    private Image modifierSpriteOverlay;    // countdown overlay fill

    ///  Whether this is a +ve value buff or not
    public bool IsPositive()
    {
        if (HPPerSecond > 0f)
            return true;

        return false;
    }

    // From potion; called by SendMessage()
    public void SetDuration(float duration)
    {
        if (duration == 0f)
            infiniteDuration = true;
        else
        {
            infiniteDuration = false;
            this.duration = duration;

            // reset timer due to duration change
            timer = 0f;
        }
    }
    // From potion; called by SendMessage()
    public void SetEffectValue(float effectValue)
    {
        HPPerSecond = (PlayerAction.instance.GetPlayerData().maxHP * effectValue) / duration;
        // keep PlayerAction.instance as playerData may not be initialized yet
    }

    // To be called when timer is up or this modifier is forcefully removed
    public void RemoveThis()
    {
        Destroy(this);
        Destroy(modifierSprite.gameObject);
        ModifiersHUD.instance.RemoveModifierFromHUD();
    }

    // Use this for initialization
    void Start()
    {
        // set reference to PlayerData
        playerData = PlayerAction.instance.GetPlayerData();

        // add to modifierHUD on top left of screen
        modifierSprite = (Image)Instantiate(ModifiersHUD.instance.modifierSpritePrefab, Vector3.zero, Quaternion.identity);
        modifierSprite.sprite = Resources.Load<Sprite>("Sprites/ModifierIcons/HPRegen");
        modifierSpriteOverlay = modifierSprite.transform.GetChild(0).GetComponent<Image>();
        modifierSpriteOverlay.fillAmount = 0f;
        ModifiersHUD.instance.AddModifierToHUD(modifierSprite.transform);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!infiniteDuration) {    // non-infinite
            // update timer
            timer += Time.deltaTime;

            // update countdown overlay for modifier icon
            modifierSpriteOverlay.fillAmount = timer / duration;

            if (timer >= duration)
                RemoveThis();

            // check whether player has reached max HP - if yes, remove this TEMPORARY regenerator
            if (playerData.IsAtMaxHP())
                RemoveThis();
        }

        if (this.tag == "Player")
        {
            playerData.RegainHP(HPPerSecond * Time.deltaTime);
        }
        else
        {
            // currently no one else has HP Regen
        }
    }

}
