using UnityEngine;
using System.Xml.Serialization;

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

    [XmlElement("effectValue")]
    public float effectValue; // effect potency of this potion
    // put as object[] ?

    // buff Component - load a script component via this Potion's name

    // non-XML variables
    private Sprite sprite;     // this potion's icon

    // Use this for initialization
    public void Init()
    {
        // load potion sprite
        sprite = Resources.Load<Sprite>("Sprites/Potions/" + spriteFilename);
    }

    public Sprite GetPotionSprite()
    {
        return sprite;
    }

}
