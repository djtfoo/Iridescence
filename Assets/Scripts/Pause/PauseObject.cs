using UnityEngine;
using System.Collections;

public class PauseObject : MonoBehaviour {

	public void SetObjectToPause(bool pause)
    {
        gameObject.SetActive(!pause);
    }

    public void SetObjectToUnpause()
    {
        gameObject.SetActive(true);
    }

}
