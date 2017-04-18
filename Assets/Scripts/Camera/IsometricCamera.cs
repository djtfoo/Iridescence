using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour {

    public GameObject player;

    Vector3 viewDir;
    float viewDist;
    Quaternion viewAngle;

    const float maxScroll = 12f;
    const float minScroll = 6f;

    // Use this for initialization
    void Start () {
        this.transform.eulerAngles = new Vector3(60f, 45f, 0);

        viewAngle = Quaternion.Euler(60f, 45f, 0);
        viewDir = viewAngle * new Vector3(0, 0, -1f);

        viewDist = 10f;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateViewDist();

        this.transform.position = player.transform.position + viewDist * viewDir;
	}

    void UpdateViewDist()
    {
        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        if (scrollVal > 0f)
        {
            viewDist = Mathf.Max(viewDist - 1f, minScroll);
        }
        else if (scrollVal < 0f)
        {
            viewDist = Mathf.Min(viewDist + 1f, maxScroll);
        }

        //viewDist = Mathf.Clamp(viewDist + scrollVal, 5f, 15f);
    }

}
