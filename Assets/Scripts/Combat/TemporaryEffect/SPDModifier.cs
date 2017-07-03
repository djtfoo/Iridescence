using UnityEngine;
using UnityEngine.UI;

public class SPDModifier : MonoBehaviour {

    public bool infiniteDuration = false;   // whether this buff will go off or not

    public float duration;  // how long this buff will last
    private float timer = 0f;   // timer to count up to the duration -- count up so it's easier for countdown overlay!

    public float changePercentage;  // percentage of how much this will change base ATK stat (0% == 0f; 50% == 0.5f; 100% == 1f)

    private float modifierValue;    // how much this component is changing the entity's ATK/damage stat

    // ModifierHUD
    private Image modifierSprite;   // to add to HUD
    private Image modifierSpriteOverlay;    // countdown overlay fill

    ///  Whether this is a +ve value buff or not
    public bool IsPositive()
    {
        if (changePercentage > 0f)
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
        changePercentage = effectValue;

        SetModifier();
    }

    // To be called when timer is up or this modifier is forcefully removed
    public void RemoveThis()
    {
        Destroy(this);
        Destroy(modifierSprite.gameObject);
        ModifiersHUD.instance.RemoveModifierFromHUD();
    }
    public void OnDestroy()
    {
        RemoveModifier();
    }

    // Use this for initialization
    void Start() {
        SetModifier();

        // add to modifierHUD on top left of screen
        modifierSprite = (Image)Instantiate(ModifiersHUD.instance.modifierSpritePrefab, Vector3.zero, Quaternion.identity);
        modifierSprite.sprite = Resources.Load<Sprite>("Sprites/ModifierIcons/SPD_Up");
        modifierSpriteOverlay = modifierSprite.transform.GetChild(0).GetComponent<Image>();
        modifierSpriteOverlay.fillAmount = 0f;
        ModifiersHUD.instance.AddModifierToHUD(modifierSprite.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!infiniteDuration) {
            // update timer
            timer += Time.deltaTime;

            // update countdown overlay for modifier icon
            modifierSpriteOverlay.fillAmount = timer / duration;

            if (timer >= duration)
                RemoveThis();
        }
    }

    private void SetModifier()
    {
        if (this.tag == "Player")
        {
            // find how much this entity's base value will be modified by
            modifierValue = PlayerAction.instance.GetPlayerData().statSPD * changePercentage;

            // modify SPD stat
            PlayerAction.instance.GetPlayerData().ChangeModifiedSPD(modifierValue);
        }
        //else if (this.tag == "Enemy")
        //{
        //    // save original defence stat
        //    prevStat = GetComponent<EnemyData>().damage;
        //    // modify defence stat
        //    GetComponent<EnemyData>().damage = prevStat * changePercentage;
        //}
    }
    private void RemoveModifier()
    {
        // restore original SPD stat
        if (this.tag == "Player")
        {
            // remove this modifier's value
            PlayerAction.instance.GetPlayerData().ChangeModifiedSPD(-modifierValue);
        }
        //else if (this.tag == "Enemy")
        //{
        //    // restore original defence stat
        //    //GetComponent<EnemyData>().damage = prevStat;
        //}
    }

}
