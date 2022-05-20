using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private static Animator anim;
    public int levelToLoad;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void FadeToLevel()
    {
        anim.SetTrigger("fade");
        // Debug.Log("Запуск анимации");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        // Debug.Log("Go");
    }
}
