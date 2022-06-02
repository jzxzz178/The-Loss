using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private static Animator anim;
    private static int levelToLoad = 4;

    public static void ChangeLevelToLoad(int lvl)
    {
        levelToLoad = lvl;
    }

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }

    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().name != "Gallery") levelToLoad = 4;
        SceneSaver.Save(SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene(levelToLoad);
        
    }
    
}
