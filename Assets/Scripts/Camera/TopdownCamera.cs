using UnityEngine;
using System.Collections;

public class TopdownCamera : MonoBehaviour {

    public bool hasPlayer;
    public GameObject player;

    float viewSize;     // current scroll amount that determines how zoomed in camera is
    float newViewSize;  // new desired view size

    // 1 scroll is about 0.5f change

    public float maxScroll = 4f; // zoom out
    public float minScroll = 2f;  // zoom in

    public float zPos = -10f;   // z-coord of this camera

    // Use this for initialization
    void Start () {
        viewSize = gameObject.GetComponent<Camera>().orthographicSize;
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
            gameObject.GetComponent<Camera>().orthographicSize = viewSize;
        }
    }

    private void LateUpdate()
    {
        if (hasPlayer /*&& player.transform.hasChanged*/) {
            transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, zPos);
            //player.transform.hasChanged = false;
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
