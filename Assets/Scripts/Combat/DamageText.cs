using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    private float timer;
    public float duration = 2f;

    public Text dmgText;

	// Use this for initialization
	void Start () {
	
	}

    public void SetDamageText(int dmg)
    {
        dmgText.text = dmg.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= duration)
            RemoveThis();

        // move upwards
        transform.position += new Vector3(0f, 0.5f * Time.deltaTime, 0f);
	}

    private void RemoveThis()
    {
        Destroy(this.gameObject);
    }

}
