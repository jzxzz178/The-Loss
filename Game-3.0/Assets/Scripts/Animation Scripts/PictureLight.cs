using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PictureLight : MonoBehaviour
{
    private Animator light;

    private void Start()
    {
        light = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            light.SetTrigger("lightOn");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetTrigger("lightOff");
        }
    }
}
