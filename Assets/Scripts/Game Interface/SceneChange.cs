using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName = "";
    private bool toChangeScene = false; // whether scene change will occur or not
    private float timeDelay = 0f;   // how much delay before scene change occurs - for playing SFX
    private float timer = 0f;   // to count up to timeDelay

    public void ChangeSceneOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneWithSFX(string SFXName)
    {
        if (AudioManager.instance)
        {
            timeDelay = AudioManager.instance.PlaySFX(SFXName);
            toChangeScene = true;
        }
    }

    private void Update()
    {
        if (toChangeScene) {
            timer += Time.deltaTime;
            if (timer >= timeDelay)
                ChangeScene(sceneName);
        }
    }

    // for non-event calls
    public static void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
