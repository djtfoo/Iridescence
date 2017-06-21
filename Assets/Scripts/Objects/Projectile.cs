using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifetime;
    Vector3 velocity;

    float duration = 0f;

    public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += velocity * Time.deltaTime;
        duration += Time.deltaTime;

        if (duration >= lifetime)
            Destroy(this.gameObject);
	}

    public void SetVelocity(Vector3 dir)
    {
        velocity = speed * dir;
    }

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("TakeDamage", 10);
            Destroy(this.gameObject);
        }
    }

}
