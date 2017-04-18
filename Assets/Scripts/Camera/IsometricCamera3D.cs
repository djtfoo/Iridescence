using UnityEngine;
using System.Collections;

// NOT IN USE!!
public class IsometricCamera3D : MonoBehaviour {

    public GameObject player;

    Vector3 viewDir;
    float viewDist;
    Quaternion viewAngle;

    const float maxScroll = 12f;
    const float minScroll = 5f;

    const float minXEuler = 30f;
    const float maxXEuler = 60f;
    float anglePerUnit;

    // Use this for initialization
    void Start()
    {
        this.transform.eulerAngles = new Vector3(45f, 45f, 0);

        viewAngle = Quaternion.Euler(45f, 45f, 0);
        viewDir = viewAngle * new Vector3(0, 0, -1f);

        viewDist = 10f;

        anglePerUnit = (maxXEuler - minXEuler) / (maxScroll - minScroll);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateViewDist();

        viewAngle = Quaternion.Euler(minXEuler + anglePerUnit * (viewDist - minScroll), 45f, 0);
        viewDir = viewAngle * new Vector3(0, 0, -1f);

        this.transform.eulerAngles = new Vector3(minXEuler + anglePerUnit * (viewDist - minScroll), 45f, 0);
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
