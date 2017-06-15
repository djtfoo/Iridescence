using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {

    // HP
    [SerializeField]
    float maxHP;
    float HP;
    Transform HPBarPrefab;
    Transform HPBar;

    // behaviour
    Vector3 velocity;

	// Use this for initialization
	void Start () {
        if (maxHP == 0)
            maxHP = 100;

        HP = maxHP;
        HPBarPrefab = Resources.Load("UserInterface/HPBar", typeof(Transform)) as Transform;

        velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
	
        // movement behaviour here
	}

    public float GetHP()
    {
        return HP;
    }

    public void TakeDamage(float dmg)
    {
        SetHP(HP - dmg);

        // edit HP bar
        if (!HPBar) {
            HPBar = (Transform)Instantiate(HPBarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            HPBar.parent = this.transform.parent;
            HPBar.position = this.transform.parent.position + HPBarPrefab.localPosition;
        }

        HPBar.GetChild(0).localScale = new Vector3(HP / maxHP, 1f, 1f);
    }

    void SetHP(float newHP)
    {
        HP = newHP;
        if (newHP <= 0)
            Destroy(this.transform.parent.gameObject);
    }
}
