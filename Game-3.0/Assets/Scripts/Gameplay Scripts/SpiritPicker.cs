using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPicker : MonoBehaviour
{
    private static int spiritCount;
    private static int maxSpiritCount;

    private AudioSource pick;
    public AudioClip clip;
    private static Animator anim;
    

    private void Start()
    {
        maxSpiritCount = GameObject.FindGameObjectsWithTag("Spirit").Length;
        spiritCount = 0;
        pick = gameObject.GetComponent<AudioSource>();
        
        anim = GameObject.FindWithTag("Gate's property").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
            pick.PlayOneShot(clip);
            Destroy(other.gameObject);
            spiritCount++;
            if (spiritCount == maxSpiritCount)
            {
                anim.SetTrigger("openDoors");
            }
        }

        if (other.gameObject.CompareTag("Door") && spiritCount == maxSpiritCount)
        {
            LevelChanger.FadeToLevel();
        }
    }
}