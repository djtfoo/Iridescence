using UnityEngine;

// script attached to GameObjects that need to be paused
public class PauseObject : MonoBehaviour {

	public void SetObjectToPause(bool pause)
    {
        gameObject.SetActive(!pause);
    }

}
