using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightScript : MonoBehaviour
{
    private GameObject player;
    private Light2D lightBulb;
    void Start()
    {
        player = gameObject.gameObject;
        for (var i = 0; i < player.transform.childCount; i++)
        {
            var child = player.transform.GetChild(i);
            if (!child.CompareTag("LightBulb")) continue;
            lightBulb = child.transform.GetChild(0).GetComponent<Light2D>();
            break;
        }
        
    }

    
    void Update()
    {
        if (lightBulb == null) return;
        if (!TotalNewControl.CheckForConnection(player))
        {
            lightBulb.intensity = 0f;
        }
        else
        {
            lightBulb.intensity = 1f;
        }
    }
}
