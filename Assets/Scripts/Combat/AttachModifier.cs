using UnityEngine;
using System;
using System.Reflection;

// Utility class for attaching modifiers
/// 1. When using a potion (for player)
/// 2. When inflicting a modifier from a skill (any entity)
public static class AttachModifier {

    // Create component for modifier & attach to entity
    /// rule of thumb:
    ///  1. if a buff of the same type already exists; and that buff is not-infinite: either add tgt or replace
    ///    - there should be only ONE non-infinite of the same type
    ///  2. if it's infinite, and the one-to-attach is not, just attach it - then player will have 2
    ///    - but, HPRegen & MPRegen should stack non-infinites too
    ///  3. if both are infinite (BUT ideally this should not occur), most likely just attach both
    ///    - only case SHOULD BE player's own passive regen + Water's MPRegen aura
    public static void SetModifierEffect(GameObject entityToAttach, string effectName, float duration, float effectValue)
    {
        // get Type of the modifier to attach
        Type modifierType = Type.GetType(effectName);

        /*
         ** Scrapped becuase component cannot be attached like a variable.
        /// Create a new GameObject to hold this potion's buff;
        /// so that SendMessage() will not send to the wrong component if player has >1 modifier
        GameObject temp = new GameObject();
        temp.AddComponent(modifierType);    // add this component
        temp.SendMessage("SetDuration", duration);
        temp.SendMessage("SetEffectValue", effectValue);
        */

        Component prevComponent = entityToAttach.GetComponent(modifierType);

        if (prevComponent == null)
            AttachToEntity(entityToAttach, effectName, duration, effectValue);
        else
            CheckReplaceModifier(entityToAttach, effectName, duration, effectValue);
    }

    /// <summary>
    ///  To check whether modifier should replace the existing one or add on.
    /// </summary>
    private static void CheckReplaceModifier(GameObject entityToAttach, string modifierType, float duration, float effectValue)
    {
        // check for whether to replace or attach as a new component
        // based off whether infinite duration or not
        switch (modifierType)
        {
            case "HPRegen":
                {
                    // can stack non-infinites; so don't check
                    AttachToEntity(entityToAttach, modifierType, duration, effectValue);
                }
                break;

            case "MPRegen":
                {
                    // can stack non-infinites; so don't check
                    AttachToEntity(entityToAttach, modifierType, duration, effectValue);
                }
                break;

            case "ATKModifier":
                {
                    // by right, there should only be one.
                    ATKModifier[] modifiersToCheck = entityToAttach.GetComponents<ATKModifier>();
                    if (duration > 0f)  // a non-infinite modifier
                    {
                        foreach (ATKModifier modifier in modifiersToCheck)
                        {
                            if (!modifier.infiniteDuration) // not infinite - there should be only 1 non-infinite
                            {
                                modifier.SetDuration(duration);
                                modifier.SetEffectValue(effectValue);
                                return;
                            }
                        }
                    }
                    // if reaches here, all existing are infinite, else this is an infinite - so attach this to entity
                    AttachToEntity(entityToAttach, modifierType, duration, effectValue);
                }
                break;

            case "DEFModifier":
                {
                    // by right, there should only be one.
                    DEFModifier[] modifiersToCheck = entityToAttach.GetComponents<DEFModifier>();
                    if (duration > 0f)  // a non-infinite modifier
                    {
                        foreach (DEFModifier modifier in modifiersToCheck)
                        {
                            if (!modifier.infiniteDuration) // not infinite - there should be only 1 non-infinite
                            {
                                modifier.SetDuration(duration);
                                modifier.SetEffectValue(effectValue);
                                return;
                            }
                        }
                    }
                    // if reaches here, all existing are infinite, else this is an infinite - so attach this to entity
                    AttachToEntity(entityToAttach, modifierType, duration, effectValue);
                }
                break;

            case "SPDModifier":
                {
                    // by right, there should only be one.
                    SPDModifier[] modifiersToCheck = entityToAttach.GetComponents<SPDModifier>();
                    if (duration > 0f)  // a non-infinite modifier
                    {
                        foreach (SPDModifier modifier in modifiersToCheck)
                        {
                            if (!modifier.infiniteDuration) // not infinite - there should be only 1 non-infinite
                            {
                                modifier.SetDuration(duration);
                                modifier.SetEffectValue(effectValue);
                                return;
                            }
                        }
                    }
                    // if reaches here, all existing are infinite, else this is an infinite - so attach this to entity
                    AttachToEntity(entityToAttach, modifierType, duration, effectValue);
                }
                break;

            case "MovementSpeedModifier":
                {
                    // by right, there should only be one.
                    MovementSpeedModifier[] modifiersToCheck = entityToAttach.GetComponents<MovementSpeedModifier>();
                    if (duration > 0f)  // a non-infinite modifier
                    {
                        foreach (MovementSpeedModifier modifier in modifiersToCheck)
                        {
                            if (!modifier.infiniteDuration) // not infinite - there should be only 1 non-infinite
                            {
                                modifier.SetDuration(duration);
                                modifier.SetEffectValue(effectValue);
                                return;
                            }
                        }
                    }
                    // if reaches here, all existing are infinite, else this is an infinite - so attach this to entity
                    AttachToEntity(entityToAttach, modifierType, duration, effectValue);
                }
                break;
        }
    }

    // 
    private static void AttachToEntity(GameObject entityToAttach, string modifierType, float duration, float effectValue)
    {
        switch (modifierType)
        {
            case "HPRegen":
                {
                    HPRegen component = entityToAttach.AddComponent<HPRegen>();
                    component.SetDuration(duration);
                    component.SetEffectValue(effectValue);
                }
                break;

            case "MPRegen":
                {
                    MPRegen component = entityToAttach.AddComponent<MPRegen>();
                    component.SetDuration(duration);
                    component.SetEffectValue(effectValue);
                }
                break;

            case "MovementSpeedModifier":
                {
                    MovementSpeedModifier component = entityToAttach.AddComponent<MovementSpeedModifier>();
                    component.SetDuration(duration);
                    component.SetEffectValue(effectValue);
                }
                break;

            case "ATKModifier":
                {
                    ATKModifier component = entityToAttach.AddComponent<ATKModifier>();
                    component.SetDuration(duration);
                    component.SetEffectValue(effectValue);
                }
                break;

            case "DEFModifier":
                {
                    DEFModifier component = entityToAttach.AddComponent<DEFModifier>();
                    component.SetDuration(duration);
                    component.SetEffectValue(effectValue);
                }
                break;

            case "SPDModifier":
                {
                    SPDModifier component = entityToAttach.AddComponent<SPDModifier>();
                    component.SetDuration(duration);
                    component.SetEffectValue(effectValue);
                }
                break;
        }
    }

}
