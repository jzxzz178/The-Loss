using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StartMenu : MonoBehaviour
{
    public Animator Slider;
    public static bool GameIsPaused = false;
    private float volume = 1f;
    private AudioSource myFx;
    public GameObject SettingsMenuUI;
    public GameObject StartButton;
    public AudioClip clickFx;
    // Start is called before the first frame update
    void Start()
    {
        myFx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        myFx.volume = volume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                SettingsExit();
            }
            else
            {
                SettingsPlay();
            }
        }

        if (GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.D) && volume < 1f)
            {
                volume += 0.2f;
            }

            if (Input.GetKeyDown(KeyCode.A) && volume > 0f)
            {
                volume -= 0.2f;
            }

            if (volume - 0 <= 10e-7)
                Slider.Play("Volume0", -1, 0.5f);
            else if (volume - 0.2f <= 10e-7)
                Slider.Play("Volume1", -1, 0.5f);
            else if (volume - 0.4f <= 10e-7)
                Slider.Play("Volume2", -1, 0.5f);
            else if (volume - 0.6f <= 10e-7)
                Slider.Play("Volume3", -1, 0.5f);
            else if (volume - 0.8f <= 10e-7)
                Slider.Play("Volume4", -1, 0.5f);
            else if (volume - 1f <= 10e-7)
                Slider.Play("Volume5", -1, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) )
        {
            ReturnToGalery();
        }
    }

    public void SettingsPlay()
    {
        ClickSound();
        GameIsPaused = true;
        SettingsMenuUI.SetActive(true);
        StartButton.SetActive(false);
    }

    public void SettingsExit()
    {
        ClickSound();
        GameIsPaused = false;
        SettingsMenuUI.SetActive(false);
        StartButton.SetActive(true);
    }

    public void ReturnToGalery()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
