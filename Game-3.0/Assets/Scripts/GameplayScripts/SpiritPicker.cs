using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPicker : MonoBehaviour
{
    public int SpiritCount;
    private int MaxSpiritCount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
            Destroy(other.gameObject);
            // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TotalNewControl>().SpiritCounterIncrement();
            SpiritCount++;
        }

        if (other.gameObject.CompareTag("Door") && SpiritCount == MaxSpiritCount)
        {
            LevelChanger.FadeToLevel();
        }
    }
}