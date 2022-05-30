using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPicker : MonoBehaviour
{
    private static int spiritCount;
    private static int maxSpiritCount;
    
    private static Animator anim;

    private void Start()
    {
        maxSpiritCount = GameObject.FindGameObjectsWithTag("Spirit").Length;
        spiritCount = 0;
        
        anim = GameObject.FindWithTag("Clouds").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
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