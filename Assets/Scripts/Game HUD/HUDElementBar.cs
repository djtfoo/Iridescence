using UnityEngine;
using UnityEngine.UI;

public class HUDElementBar : MonoBehaviour {
    
    public Transform element1;  // bar for element 1
    public Transform element2;  // bar for element 2

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Element1Changed(Element element)
    {
        element1.GetComponent<Image>().color = element.color;
    }

    public void Element2Changed(Element element)
    {
        element2.GetComponent<Image>().color = element.color;
    }

    public void ElementCharge1Changed(int newVal)
    {
        element1.localScale = new Vector3(-newVal / 100f, 1f, 1f);
    }

    public void ElementCharge2Changed(int newVal)
    {
        element2.localScale = new Vector3(newVal / 100f, 1f, 1f);
    }

}
