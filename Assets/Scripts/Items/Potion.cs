using UnityEngine;
using System.Xml.Serialization;
using System;

[XmlRoot("Potion")]
public class Potion {

    [XmlElement("name")]
    public string name;     // this potion's name - also used for this potion's buff component

    [XmlElement("description")]
    public string description;  // this potion's description

    [XmlElement("spriteFilename")]
    public string spriteFilename; // filename of this potion's sprite

    [XmlElement("craftCost")]
    public int craftCost;   // number of blank shards required for craft

    [XmlElement("duration")]
    public float duration; // duration (in seconds) of the effect of this potion

    [XmlElement("cooldownTime")]
    public float cooldownTime;  // how long this potion is on cooldown for after use

    [XmlElement("effectName")]
    public string effectName;    // name of effect modifier (script) of this potion

    [XmlElement("effectValue")]
    public float effectValue; // effect potency of this potion
    // put as object[] ?

    // buff Component - load a script component via this Potion's effectName

    // non-XML variables
    private Sprite sprite;     // this potion's icon

    // cooldown-related
    private float timer;    // this potion's timer to count up to cooldown
    private bool updatedThisFrame;  // this potion's timer already updated once this frame

    // reference to PlayerData
    private PlayerData playerData;

    // Use this for initialization
    public void Init()  // XML already deserialized
    {
        // load potion sprite
        sprite = Resources.Load<Sprite>("Sprites/Potions/" + spriteFilename);
        timer = cooldownTime;
        updatedThisFrame = false;

        // set reference to PlayerData
        playerData = PlayerAction.instance.GetPlayerData();
    }

    public Sprite GetPotionSprite()
    {
        return sprite;
    }

    public void UsePotion()
    {
        AttachModifier.SetModifierEffect(PlayerAction.instance.gameObject, effectName, duration, effectValue);

        // set cooldown to start
        timer = 0f;
    }

    public bool IsOnCooldown()
    {
        if (timer < cooldownTime)
            return true;

        return false;
    }

    public void ResetUpdateThisFrame()
    {
        updatedThisFrame = false;
    }

    public void UpdateCooldown()
    {
        if (updatedThisFrame)
            return;

        timer += Time.deltaTime;
        if (timer >= cooldownTime)
            timer = cooldownTime;    // set complete at 1

        // update fill for PotionsHUD
        for (int i = 0; i < 5; ++i)
        {
            if (playerData.equippedPotions[i] == name)
                PotionsHUD.instance.potionSprites[i].fillAmount = timer / cooldownTime;
        }

        updatedThisFrame = true;
    }

}