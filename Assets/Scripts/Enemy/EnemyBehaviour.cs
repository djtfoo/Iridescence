using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Xml.Serialization;

[XmlRoot(ElementName = "EnemyBehaviour")]
public class EnemyBehaviour {

    [XmlElement(ElementName = "methodName")]
    public string methodName;

    [XmlArray("parameters")]
    [XmlArrayItem("ObjectArrayItem")]
    public ObjectArrayItem[] parameters;

    public object[] methodParams;   // will be populated with data from ObjectArrayItem[]

    public void AddEnemy(GameObject enemy)
    {
        Array.Resize(ref methodParams, methodParams.Length + 1);
        methodParams[methodParams.Length - 1] = enemy;
    }

    public void Update()
    {
        //Debug.Log(methodParams[0].GetType());

        //Get method information
        MethodInfo method = GetType().GetMethod(methodName);    // from this class, EnemyBehaviour

        if (method != null)
        {
            //Invoke the method
            method.Invoke(this, methodParams);
        }

    }

    public void ChasePlayer(float speed, GameObject enemy)
    {
        Debug.Log("YO");
    }

}
