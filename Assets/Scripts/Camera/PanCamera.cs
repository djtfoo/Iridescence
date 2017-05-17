using UnityEngine;
using System.Collections;

// Pan orthographic camera by bringing mouse to edge of screen
public class PanCamera : MonoBehaviour {

    int dirX;
    int dirY;

    int screenWidth;
    int screenHeight;

    void Start()
    {
        dirX = 0;
        dirY = 0;

        screenWidth = Screen.width - 1;
        screenHeight = Screen.height - 1;
    }

    void Update()
    {
        if (Input.mousePosition.x >= screenWidth)
        {
            dirX = 1;
        }
        else if (Input.mousePosition.x <= 0)
        {
            dirX = -1;
        }
        if (Input.mousePosition.y >= screenHeight)
        {
            dirY = 1;
        }
        else if (Input.mousePosition.y <= 0)
        {
            dirY = -1;
        }


        if (dirX != 0 || dirY != 0) {
            transform.localPosition = new Vector3(transform.localPosition.x + dirX * Time.deltaTime * 5f, transform.localPosition.y + dirY * Time.deltaTime * 5f, transform.localPosition.z);
            dirX = 0;
            dirY = 0;
        }
    }

}
