using UnityEngine;
using System.Collections;
using System;

public abstract class Requirement {

    //bool isMet;

    public abstract bool IsConditionMet();
    public abstract string GetRequirementString();
    public abstract string GetFulfillmentString();

}

public class RequirementKillMonster : Requirement {

    string monsterName;
    int quantity;

    public override bool IsConditionMet()
    {
        throw new NotImplementedException();
    }

    public override string GetRequirementString()
    {
        string requirement = "Kill " + quantity.ToString() + " " + monsterName;
        if (quantity > 1)
            requirement += "s";

        return requirement;
    }
    public override string GetFulfillmentString()
    {
        throw new NotImplementedException();
    }

}

public class RequirementCollectItem : Requirement {

    string itemName;
    int quantity;

    public override bool IsConditionMet()
    {
        throw new NotImplementedException();
    }

    public override string GetRequirementString()
    {
        string requirement = "Collect " + quantity.ToString() + " " + itemName;
        if (quantity > 1)
            requirement += "s";

        return requirement;
    }
    public override string GetFulfillmentString()
    {
        throw new NotImplementedException();
    }

}

public class RequirementReachLocation : Requirement {

    string locationName;

    public override bool IsConditionMet()
    {
        throw new NotImplementedException();
    }

    public override string GetRequirementString()
    {
        return "Reach " + locationName;
    }
    public override string GetFulfillmentString()
    {
        throw new NotImplementedException();
    }

}