using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerLevelSelect : MonoBehaviour
{
    private bool availableToSwitchLevel;
    private LevelChanger levelChanger;

    public void Start()
    {
        levelChanger = GameObject.FindWithTag("LevelChanger").GetComponent<LevelChanger>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Picture1"))
        {
            availableToSwitchLevel = true;
            LevelChanger.ChangeLevelToLoad(2);
        }

        if (other.gameObject.CompareTag("Picture2"))
        {
            availableToSwitchLevel = true;
            LevelChanger.ChangeLevelToLoad(3);
        }

        if (other.gameObject.CompareTag("Picture3"))
        {
            availableToSwitchLevel = true;
            LevelChanger.ChangeLevelToLoad(4);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && availableToSwitchLevel)
            LevelChanger.FadeToLevel();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        availableToSwitchLevel = false;
    }
}