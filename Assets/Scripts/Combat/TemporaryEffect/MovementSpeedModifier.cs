using UnityEngine;
using System.Collections;

public class MovementSpeedModifier : MonoBehaviour {

    public bool infiniteDuration = false;   // whether this buff will go off or not

    public float duration;  // how long this buff will last
    private float timer = 0f;   // timer to count up to the duration -- count up so it's easier for countdown overlay!

    public float speedPercentage;   // percentage of how much change in speed (0% == 0f; 100% == 1f; 105% = 1.05f)

    private Vector3 previousPos;    // this entity's previous position

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
        speedPercentage = 1f + effectValue;
    }

    // To be called when timer is up or this modifier is forcefully removed
    public void RemoveThis()
    {
        Destroy(this);
    }

    // Use this for initialization
    void Start()
    {
        previousPos = transform.position;
        // add to buff list on top left of screen
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!infiniteDuration) {
            // update timer
            timer += Time.deltaTime;

            // update countdown overlay for buff icon
             
            if (timer >= duration)
                RemoveThis();
        }

        if (previousPos != transform.position)  // entity has moved
        {
            Vector3 distMoved = transform.position - previousPos;
            transform.position = previousPos + speedPercentage * distMoved;
            previousPos = transform.position;
        }
    }

}
