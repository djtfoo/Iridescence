using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

/// <summary>
/// Requirement Class that objects are initialised as
/// Requirements will call the override functions of specific requirements
/// </summary>
public class Requirement {

    //bool isMet;

    private string requirementType; // typeof (name of the derived class)
    private object[] funcParams;

    /// <summary>
    /// @desc  Virtual function to be defined by sub-classes
    /// </summary>
    /// <param name="funcParams"> Variables unique to the class </param>
    /// <returns> Whether condition is met or not </returns>
    public bool IsConditionMet()
    {
        switch (requirementType)
        {
            case "KillMonster":
                Type type = typeof(KillMonster);
                MethodInfo method = type.GetMethod("ConditionMet");
                KillMonster invoker = new KillMonster();
                method.Invoke(invoker, funcParams);
                break;

            default:
                break;
        }

        return false;
    }
    public string GetRequirementString()
    {
        return "Requirement";
    }
    public string GetFulfillmentString()
    {
        return "Fulfilled";
    }

}

public class KillMonster : Requirement {

    //string monsterName;
    //int quantity;

    public bool ConditionMet(object[] funcParams)
    {
        throw new NotImplementedException();
    }

    public string GetRequirementString(string monsterName, int quantity)
    {
        string requirement = "Kill " + quantity.ToString() + " " + monsterName;
        if (quantity > 1)
            requirement += "s";
        
        return requirement;
    }
    public string GetFulfillmentString(object[] funcParams)
    {
        throw new NotImplementedException();
    }

}

public class CollectItem : Requirement {

    //string itemName;
    //int quantity;

    public bool IsConditionMet(object[] funcParams)
    {
        throw new NotImplementedException();
    }

    public string GetRequirementString(string itemName, int quantity)
    {
        string requirement = "Collect " + quantity.ToString() + " " + itemName;
        if (quantity > 1)
            requirement += "s";
        
        return requirement;
    }
    public string GetFulfillmentString(object[] funcParams)
    {
        throw new NotImplementedException();
    }

}

public class ReachLocation : Requirement {

    //string locationName;

    public bool IsConditionMet(object[] funcParams)
    {
        throw new NotImplementedException();
    }

    public string GetRequirementString(string locationName)
    {
        return "Reach " + locationName;
    }
    public string GetFulfillmentString(object[] funcParams)
    {
        throw new NotImplementedException();
    }

}