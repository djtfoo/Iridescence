using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

    private SpriteRenderer thisRenderer;    // this shadow's renderer
    public SpriteRenderer entitySpriteRenderer; // entity to retrieve sprite from

    private bool isFirstFrame = true;   // set position during first frame, after sprite is rendered

	// Use this for initialization
	void Start () {
        thisRenderer = GetComponent<SpriteRenderer>();

        // set position properly in case it's not
        //transform.localPosition = new Vector3(0.2f, 0.16f, 1f);   // for 64x64

        //transform.eulerAngles = new Vector3(50f, 330f, 300f);
        //transform.localScale = new Vector3(1f, 0.9f, 1f);

        transform.eulerAngles = new Vector3(60f, 30f, 0f);
        transform.localScale = new Vector3(1f, 0.9f, 1f);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (thisRenderer && entitySpriteRenderer)
        {
            thisRenderer.sprite = entitySpriteRenderer.sprite;

            if (isFirstFrame) { // set position
                Vector3 boundsSize = thisRenderer.sprite.bounds.size;
                /// size is: divide by 400 (0.25 times)
                //transform.localPosition = new Vector3(boundsSize.x * 0.25f, boundsSize.y * 0.25f, DepthSortManager.GetZUnitPerY() * 0.01f);

                /// size is: X = 0.135f, Y = 0.145f, for a 64x64 sprite
                transform.localPosition = new Vector3(boundsSize.x * 0.215f, boundsSize.y * 0.228f, DepthSortManager.GetZUnitPerY() * 0.01f);
            }
        }
	}
}
