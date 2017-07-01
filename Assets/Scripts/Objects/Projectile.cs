using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifetime;
    private Vector3 velocity;

    public float damage;

    private float duration = 0f;
    private bool hit;   // this projectile has hit an enemy
    private GameObject hitTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += velocity * Time.deltaTime;
        duration += Time.deltaTime;

        if (hit)
        {
            if (duration >= 0.3f) {
                hitTarget.SendMessage("TakeDamage", damage);
                Destroy(this.gameObject);
            }
        }
        else if (duration >= lifetime)
            Destroy(this.gameObject);
	}

    public void SetVelocity(Vector3 dir)
    {
        velocity = speed * dir;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Player" && collision.gameObject.tag == "Enemy") {
            hit = true;
            duration = 0f;  // reset timer - countdown to destruction instead
            hitTarget = collision.gameObject;
        }
        //else if (this.gameObject.tag == "Enemy" && collision.gameObject.tag == "Player")
        //{
        //    hit = true;
        //    duration = 0f;  // reset timer - countdown to destruction instead
        //    hitTarget = PlayerAction.instance.gameObject;
        //}
    }

}
