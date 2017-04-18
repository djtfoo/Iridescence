using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update() {
        Vector3 quadToCamera = camera.transform.position - this.transform.position;
        Vector3 quadToCameraGround = new Vector3(quadToCamera.x, this.transform.position.y, quadToCamera.z);

        Vector3 quadFront = new Vector3(0, 0, -1f);

        float eulerX = 180f - Mathf.Atan2(quadToCamera.y - quadToCameraGround.y, quadToCamera.x) * Mathf.Rad2Deg;
        float eulerY = Mathf.Atan2(quadToCamera.z, quadToCamera.x) * Mathf.Rad2Deg - 180f;  // this is causing the sprite to rotate

        this.transform.eulerAngles = new Vector3(eulerX, eulerY, 0f);
    }
}
