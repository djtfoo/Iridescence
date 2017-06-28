using UnityEngine;
using UnityEngine.UI;

// Simple information of a GameObject the user has mouse over
public class HUDHighlightInfo : MonoBehaviour {

    [Tooltip("GameObject's name")]
    public Text gameObjectName;

    [Tooltip("GameObject's object type")]
    public Text gameObjectType;

    // Use this for initialization
    void Start () {

        gameObject.SetActive(false);
    }
	
    public void SetHighlightInfo(string name, string tag)
    {
        gameObject.SetActive(true);

        gameObjectName.text = name;
        gameObjectType.text = tag;
    }

    public void DeactivateHighlightInfo()
    {
        gameObject.SetActive(false);
    }


}
