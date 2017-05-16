using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName = "";

    public void ChangeSceneOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
