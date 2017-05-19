using UnityEngine;
using System.Collections;

public enum GO_TYPE
{
    GO_TERRAIN,
    GO_NPC,
    GO_PROP,
    GO_NIL
}

public class AssetInfo : MonoBehaviour {

    [SerializeField]
    int ID = 0;    // type of tile; 0 is default empty/blank
    [SerializeField]
    GO_TYPE gotype = GO_TYPE.GO_NIL;

    public void SetAssetInfo(int id, GO_TYPE type)
    {
        ID = id;
        gotype = type;
    }

    // ID
    public void SetID(int id) {
        ID = id;
    }
    public int GetID() {
        return ID;
    }

    // GO_TYPE
    public void SetGOType(GO_TYPE type)
    {
        gotype = type;
    }
    public GO_TYPE GetGOType()
    {
        return gotype;
    }

}
