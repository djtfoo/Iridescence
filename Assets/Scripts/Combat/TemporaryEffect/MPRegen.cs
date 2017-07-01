using UnityEngine;
using System.Collections;

public class MPRegen : MonoBehaviour {

    public bool infiniteDuration = false;   // whether this buff will go off or not
    public float duration;  // how long this buff will last
    private float timer = 0f;   // timer to count up to the duration -- count up so it's easier for countdown overlay!

    [Tooltip("Amount of MP to regain per second")]
    public float MPPerSecond;   // amount to regain per second

    // reference to PlayerData
    private PlayerData playerData;

    ///  Whether this is a +ve value buff or not
    public bool IsPositive()
    {
        if (MPPerSecond > 0f)
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
        MPPerSecond = (PlayerAction.instance.GetPlayerData().maxHP * effectValue) / duration;
        // keep PlayerAction.instance as playerData may not be initialized yet
    }

    // To be called when timer is up or this modifier is forcefully removed
    public void RemoveThis()
    {
        Destroy(this);
    }

    // Use this for initialization
    void Start () {
        // set reference to PlayerData
        playerData = PlayerAction.instance.GetPlayerData();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!infiniteDuration) {    // non-infinite
            // update timer
            timer += Time.deltaTime;

            // update countdown overlay for buff icon

            if (timer >= duration)
                RemoveThis();

            // check whether player has reached max MP - if yes, remove this TEMPORARY regenerator
            if (playerData.IsAtMaxMP())
                RemoveThis();
        }

        if (this.tag == "Player")
        {
            playerData.RegainMP(MPPerSecond * Time.deltaTime);
        }
        else
        {
            // currently no one else has MP Regen
        }
    }

}
