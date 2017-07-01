using UnityEngine;
using System.Reflection;
using System;

public class EnemyData : MonoBehaviour {

    // HP
    [SerializeField]
    private float maxHP;
    private float HP;
    private Transform HPBarPrefab;
    private Transform HPBar;

    // Enemy stats
    public float damage;    // how much damage this enemy deals

    // Enemy behaviour
    public TextAsset behaviourXML;  // XML file containing behaviour data
    private EnemyBehaviour behaviour;

    Vector3 velocity;

	// Use this for initialization
	void Start () {
        if (maxHP == 0)
            maxHP = 100;    // default

        HP = maxHP;
        HPBarPrefab = Resources.Load("UserInterface/HPBar", typeof(Transform)) as Transform;

        velocity = Vector3.zero;
        behaviour = new EnemyBehaviour();
        // read enemy behaviour from XML file
        //XmlSerializer serializer = new XmlSerializer(typeof(EnemyBehaviour));
        //using (System.IO.StringReader reader = new System.IO.StringReader(behaviourXML.text))
        //{
        //    behaviour = serializer.Deserialize(reader) as EnemyBehaviour;
        //}

        behaviour = XMLSerializer<EnemyBehaviour>.DeserializeXMLFile(behaviourXML);
        behaviour.methodParams = XMLSerializer<EnemyBehaviour>.ObjectArrayItemToObjectArray(behaviour.parameters);
        behaviour.AddEnemy(gameObject); // pass this enemy over to enemy behaviour to handle
    }

    // Update is called once per frame
    void Update () {

        // movement behaviour here
        behaviour.Update();

        //Type type = typeof(EnemyBehaviour);
        //MethodInfo method = type.GetMethod(behaviour.methodName);
        //method.Invoke(behaviour, behaviour.methodParams);

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
        if (newHP <= 1f)    // cause HP is in float; will round down to 0
            Destroy(this.transform.parent.gameObject);
        else
            HP = newHP;
    }
}
