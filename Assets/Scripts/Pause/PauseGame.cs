using UnityEngine;
using System.Collections;

// script attached to PauseManager object
public class PauseGame : MonoBehaviour {

    static bool isPaused = false;
    PauseObject[] objects = new PauseObject[0];

    //public GameObject pauseCanvasPrefab;
    public GameObject pauseCanvas;
    public GameObject menuStats;    // sub-menu for stats
    public GameObject menuSkills;   // sub-menu for elements & skills
    public GameObject menuQuests;   // sub-menu for quests
    public GameObject menuAchievements; // sub-menu for achievements

    // Use this for initialization
    void Start () {
        //pauseCanvas = (GameObject)Instantiate(pauseCanvasPrefab, Vector3.zero, Quaternion.identity);

        //pauseCanvas = transform.GetChild(0).gameObject;

        pauseCanvas.SetActive(false);
    }
	
    /// <summary>
    ///  Function called by button click to pause/unpause the game
    /// </summary>
    /// <param name="pause"> true to pause, false to unpause </param>
    public void SetPause(bool pause)
    {
        isPaused = pause;

        if (objects.Length == 0)
            objects = FindObjectsOfType(typeof(PauseObject)) as PauseObject[];

        foreach (PauseObject setPause in objects)
        {
            setPause.SetObjectToPause(pause);
        }

        pauseCanvas.SetActive(pause);
    }

    /// <summary>
    ///  Function called by button click to set showing of stats menu
    /// </summary>
    public void SetStatsMenu()  // default menu for pause
    {
        menuStats.SetActive(true);
        menuSkills.SetActive(false);
        menuQuests.SetActive(false);
        menuAchievements.SetActive(false);

        menuStats.GetComponent<StatsMenu>().SetStats();
    }

    /// <summary>
    ///  Function called by button click to set showing of skills menu
    /// </summary>
    public void SetSkillsMenu()
    {
        menuStats.SetActive(false);
        menuSkills.SetActive(true);
        menuQuests.SetActive(false);
        menuAchievements.SetActive(false);
    }

    /// <summary>
    ///  Function called by button click to set showing of quest menu
    /// </summary>
    public void SetQuestsMenu()
    {
        menuStats.SetActive(false);
        menuSkills.SetActive(false);
        menuQuests.SetActive(true);
        menuAchievements.SetActive(false);
    }

    /// <summary>
    ///  Function called by button click to set showing of achievements menu
    /// </summary>
    public void SetAchievementsMenu()
    {
        menuStats.SetActive(false);
        menuSkills.SetActive(false);
        menuQuests.SetActive(false);
        menuAchievements.SetActive(true);
    }

}
