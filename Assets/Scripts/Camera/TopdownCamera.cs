using UnityEngine;
using System.Collections;

public class TopdownCamera : MonoBehaviour {

    public GameObject player;

    float viewDist;     // scroll amount that determines how zoomed in camera is

    const float maxScroll = 12f;
    const float minScroll = 6f;

    // Use this for initialization
    void Start () {
        viewDist = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateViewDist();

        this.transform.position = player.transform.position - new Vector3(0f, 0f, viewDist);
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
