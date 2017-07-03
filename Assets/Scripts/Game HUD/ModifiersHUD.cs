using UnityEngine;
using UnityEngine.UI;

public class ModifiersHUD : MonoBehaviour {

    public static ModifiersHUD instance;

    // reference to prefab
    public Image modifierSpritePrefab;  // 1 modifier icon to add to list
    public float spriteWidth;
    public float spacing;

    private void Awake()
    {
        instance = GetComponent<ModifiersHUD>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddModifierToHUD(Transform modifier)
    {
        // add as child of this
        modifier.SetParent(transform);    // set this PotionInventory as parent
        modifier.localScale = new Vector3(1f, 1f, 1f);

        // expand width
        transform.GetComponent<RectTransform>().sizeDelta += new Vector2(spriteWidth + spacing, 0f);
    }

    public void RemoveModifierFromHUD()
    {
        // reduce width
        transform.GetComponent<RectTransform>().sizeDelta -= new Vector2(spriteWidth + spacing, 0f);
    }

}
