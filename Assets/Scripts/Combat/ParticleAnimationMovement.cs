using UnityEngine;
using System.Collections;

public class ParticleAnimationMovement : MonoBehaviour {

    public bool translateUpwards;
    public bool rotate;
    public bool scale;

    Transform parent = null;
    Vector3 prevParentPos = Vector3.zero;

	// Use this for initialization
	void Start () {
        
    }

    public void SetParent(Transform parent)
    {
        this.parent = parent;
        prevParentPos = parent.position;
    }
	
	// Update is called once per frame
	void Update () {
	
        if (parent != null) // follow parent
        {
            Vector3 relativeMovement = parent.position - prevParentPos;
            transform.position += relativeMovement;

            prevParentPos = parent.position;    // update the previous position
        }

        if (translateUpwards)
        {
            transform.position += new Vector3(0f, 0.15f * Time.deltaTime);
        }

        if (rotate)
        {
            transform.Rotate(new Vector3(0f, 30f * Time.deltaTime, 0f));
        }

        if (scale)
        {
            transform.localScale += 0.2f * new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
        }

	}
}
