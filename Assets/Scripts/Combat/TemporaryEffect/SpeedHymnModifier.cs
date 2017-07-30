using UnityEngine;
using UnityEngine.UI;

public class SpeedHymnModifier : MonoBehaviour {

    public bool infiniteDuration = false;   // whether this buff will go off or not

    public float duration;  // how long this buff will last
    private float timer = 0f;   // timer to count up to the duration -- count up so it's easier for countdown overlay!

    public float speedPercentage;   // percentage of how much change in speed (0% == 0f; 100% == 1f; 105% = 1.05f)

    private Vector3 previousPos;    // this entity's previous position

    // ModifierHUD
    private Image modifierSprite;   // to add to HUD
    private Image modifierSpriteOverlay;    // countdown overlay fill

    ///  Whether this is a +ve value buff or not
    public bool IsPositive()
    {
        if (speedPercentage > 1f)
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
        speedPercentage = effectValue;
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
        previousPos = transform.position;

        // add to modifierHUD on top left of screen
        modifierSprite = (Image)Instantiate(ModifiersHUD.instance.modifierSpritePrefab, Vector3.zero, Quaternion.identity);
        modifierSprite.sprite = Resources.Load<Sprite>("Sprites/ModifierIcons/SpeedHymn");
        modifierSpriteOverlay = modifierSprite.transform.GetChild(0).GetComponent<Image>();
        modifierSpriteOverlay.fillAmount = 0f;
        ModifiersHUD.instance.AddModifierToHUD(modifierSprite.transform);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!infiniteDuration)
        {
            // update timer
            timer += Time.deltaTime;

            // update countdown overlay for modifier icon
            modifierSpriteOverlay.fillAmount = timer / duration;

            if (timer >= duration)
                RemoveThis();
        }

        if (previousPos != transform.position)  // entity has moved
        {
            if (PlayerAction.instance.IsMovingThisFrame())
            {
                Vector3 distMoved = transform.position - previousPos;
                transform.position += speedPercentage * distMoved;
                PlayerAction.instance.SetEndMovingThisFrame();
            }
            previousPos = transform.position;
        }
    }

}
