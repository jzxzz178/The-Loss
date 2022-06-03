using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimeLineMenedgerSc : MonoBehaviour
{
    public PlayableDirector Director;
   // private AudioSource myfx;
    // Start is called before the first frame update
    void Start()
    {
       // myfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //myfx.volume = 1;
        if (Director.state != PlayState.Playing)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
    }
}
