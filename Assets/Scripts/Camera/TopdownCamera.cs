using UnityEngine;
using System.Collections;

public class TopdownCamera : MonoBehaviour {

    public GameObject player;

    float viewSize;     // current scroll amount that determines how zoomed in camera is
    float newViewSize;  // new desired view size

    const float maxScroll = 5f; // zoom out
    const float minScroll = 2f;  // zoom in

    // Use this for initialization
    void Start () {
        viewSize = this.gameObject.GetComponent<Camera>().orthographicSize;
        newViewSize = viewSize;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateViewDist();
        if (viewSize != newViewSize) {
            viewSize += (newViewSize - viewSize) * 5f * Time.deltaTime;
            if (Mathf.Abs(newViewSize - viewSize) < 0.001f)
                viewSize = newViewSize;
            this.gameObject.GetComponent<Camera>().orthographicSize = viewSize;
        }
    }

    void UpdateViewDist()
    {
        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        if (scrollVal > 0f)
        {
            newViewSize = Mathf.Max(viewSize - 0.5f, minScroll);
            //this.gameObject.GetComponent<Camera>().orthographicSize = viewSize;
        }
        else if (scrollVal < 0f)
        {
            newViewSize = Mathf.Min(viewSize + 0.5f, maxScroll);
            //this.gameObject.GetComponent<Camera>().orthographicSize = viewSize;
        }

        //viewDist = Mathf.Clamp(viewDist + scrollVal, 5f, 15f);
    }
}
