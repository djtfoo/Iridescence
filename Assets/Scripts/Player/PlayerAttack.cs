using UnityEngine;
using System.Collections;

// temp
public enum ATK_TYPE
{
    ATK_MELEE,
    ATK_FIREPROJECTILE
}

public class PlayerAttack : MonoBehaviour {

    public float meleeRangeSquared = 0.5f;  // temp variable to represent weapon
    public float meleeDmg = 10f;    // temp damage variable

    public ATK_TYPE attackType = ATK_TYPE.ATK_MELEE;    // current attack type
    public float rangedRangeSquared = 1.5f;  // temp variable to represent ranged attack range

    public GameObject fireProjectile;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnProjectile(Vector3 targetPos)
    {
        GameObject projectile = (GameObject)Instantiate(fireProjectile, this.transform.GetChild(0).position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetVelocity((targetPos - this.transform.position).normalized);
    }

}
