using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

/// <summary>
///  A struct for serialising an object[] in a class
/// </summary>
public struct ObjectArrayItem
{
    [XmlAttribute("varType")]
    public string varType;  // type of the variable of this object item

    [XmlElement("variable")]
    public string variable; // the variable to be converted to the varType
}

public static class XMLSerializer<T> where T : class {

    public static T DeserializeXMLFile(TextAsset XMLfile)
    {
        var serializer = new XmlSerializer(typeof(T));
        return serializer.Deserialize(new StringReader(XMLfile.text)) as T;
    }

    public static object[] ObjectArrayItemToObjectArray(ObjectArrayItem[] OAIarr)
    {
        object[] arr = new object[OAIarr.Length];
        for (int i = 0; i < OAIarr.Length; ++i)
        {
            switch (OAIarr[i].varType)
            {
                case "int":
                    {
                        float variable = int.Parse(OAIarr[i].variable);
                        arr[i] = variable;
                    }
                    break;
                case "float":
                    {
                        float variable = float.Parse(OAIarr[i].variable);
                        arr[i] = variable;
                    }
                    break;

                // for Skills
                case "Damage":  // float
                    {
                        float variable = float.Parse(OAIarr[i].variable);
                        arr[i] = variable;
                    }
                    break;

                case "CriticalChanceIncrease":
                    {
                        float variable = float.Parse(OAIarr[i].variable);
                        arr[i] = variable;
                    }
                    break;

                case "ComponentSelf":
                    arr[i] = OAIarr[i].variable;
                    break;

                case "ComponentEnemy":
                    arr[i] = OAIarr[i].variable;
                    break;

                case "Duration":    // float
                    {
                        float variable = float.Parse(OAIarr[i].variable);
                        arr[i] = variable;
                    }
                    break;

                case "EffectValue": // float
                    {
                        float variable = float.Parse(OAIarr[i].variable);
                        arr[i] = variable;
                    }
                    break;

                case "SpawnName":
                    arr[i] = OAIarr[i].variable;
                    break;

                default:
                    break;
            }
        }

        return arr;
    }


    public static Dictionary<string, string> ObjectArrayItemToDictionary(ObjectArrayItem[] OAIarr)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        for (int i = 0; i < OAIarr.Length; ++i)
        {
            dict.Add(OAIarr[i].varType, OAIarr[i].variable);
        }

        return dict;
    }

}
