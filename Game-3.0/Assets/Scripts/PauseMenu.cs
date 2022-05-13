using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject puseMenuUI;
    public GameObject settingsMenuUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        puseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        puseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
        
        // Debug.Log("Quit");
        // Application.Quit();
    }

    public void ReStart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        
    }

    public void SettingsPlay()
    {
        puseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void SettingsExit()
    {
        puseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void ReturnToGalery()
    {
        Resume();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
