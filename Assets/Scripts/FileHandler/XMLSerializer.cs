using UnityEngine;
using System.Xml.Serialization;
using System.IO;

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
                case "float":
                    float variable = float.Parse(OAIarr[i].variable);
                    arr[i] = variable;
                    break;

                default:
                    break;
            }
        }

        return arr;
    }

}
