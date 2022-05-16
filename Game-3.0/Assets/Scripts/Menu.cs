
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;
    public static int ButtonIndex = 0;
    public bool MenuIncluded = false;
    public Animator Pointer;
    //public AudioSource myFx;
    private AudioSource myFx;
    public AudioClip levelStartFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public Animator Slider;
    public Animator Picture1;
    public Animator Picture2;
    public AudioClip GaleryOpen;
    public int pictureNumber = 0;
    private float volume;
    private bool pictureIsAnimated = false;

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        myFx = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            LevelStartSound();
        }
        //else if (SceneManager.GetActiveScene().buildIndex == 1)
           // myFx.PlayOneShot(GaleryOpen);
    }

    // Update is called once per frame
    void Update()
    {
       
        myFx.volume = volume;
        if (!GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && pictureNumber == 1)
            {
                PlayLevelTwo();
            }
            if (Input.GetKeyDown(KeyCode.Space) && pictureNumber == 0)
            {
                PlayLevelOne();
            }
            

            if (Input.GetKeyDown(KeyCode.A) && pictureNumber == 1)
            {
                HoverSound();
                pictureNumber = 0;
                pictureIsAnimated = false;
                //Debug.Log(pictureNumber);
            }

            if (Input.GetKeyDown(KeyCode.D) && pictureNumber == 0)
            {
                HoverSound();
                pictureNumber = 1;
                pictureIsAnimated = false;
                // Debug.Log(pictureNumber);
            }
            if (pictureNumber == 0 && !pictureIsAnimated)
            {
                Picture2.enabled = false;
                Picture1.enabled = true;
                Picture1.Play("choice1", -1, 0f);
                pictureIsAnimated = true;
            }
            else if (!pictureIsAnimated)
            {
                Picture1.enabled = false;
                Picture2.enabled = true;
                Picture2.Play("choice2", -1, 0f);
                pictureIsAnimated = true;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (MenuIncluded)
                {
                    HoverSound();
                    Resume();
                }
                else
                    SettingsExit();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && MenuIncluded && ButtonIndex + 1 <= 2)
        {
            HoverSound();
            ButtonIndex = ButtonIndex + 1;
        }

        if (Input.GetKeyDown(KeyCode.W) && MenuIncluded && ButtonIndex - 1 >= 0)
        {
            HoverSound();
            ButtonIndex = ButtonIndex - 1;
        }

        if (!MenuIncluded && GameIsPaused)
        {
            pictureIsAnimated = false;
            if (Input.GetKeyDown(KeyCode.D) && volume  < 1f)
            {
                volume += 0.2f;
            }

            if (Input.GetKeyDown(KeyCode.A) && volume > 0f)
            {
                volume -= 0.2f;  
            }

            PlayerPrefs.SetFloat("Volume", volume);

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

        

        if (Input.GetKeyDown(KeyCode.Space) && MenuIncluded)
        {
            switch (ButtonIndex)
            {
                case 0:
                    ClickSound();
                    Resume();
                    break;
                case 1:
                    SettingsPlay();
                    break;
                case 2:
                    QuitGame();
                    break;
            }

        }

        if (MenuIncluded)
        {
            switch (ButtonIndex)
            {
                case 0:
                    Pointer.Play("ContinueChoice", -1, 0.5f);
                    break;
                case 1:
                    Pointer.Play("SettingsChoice", -1, 0.5f);
                   // Debug.Log("да");
                    break;
                case 2: 
                    Pointer.Play("ExitChoice", -1, 0.5f);                                      
                    break;
            }
        }
    }

    public void Resume()
    {
        
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        MenuIncluded = false;
    }

    public void Pause()
    {
        HoverSound();
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        MenuIncluded = true;
    }

    public void QuitGame()
    {
        //Resume();
        // Debug.Log("Quit");
        ClickSound();
        Application.Quit();
    }

    public void ReStart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void SettingsPlay()
    {
        ClickSound();
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
        MenuIncluded = false;
    }

    public void SettingsExit()
    {
        HoverSound();
        MenuIncluded = true;
        PauseMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
    }

    public void ReturnToGalery()
    {
        Resume();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void PlayLevelOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2, LoadSceneMode.Single);
    }
    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }

    public void LevelStartSound()
    {
        myFx.PlayOneShot(levelStartFx);
    }
}


