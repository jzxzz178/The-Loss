using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TotalNewControl>().SpiritCounterIncrement();
        }

        if (other.gameObject.CompareTag("Door"))
        {

            LevelChanger.FadeToLevel();

        }
    }
}
