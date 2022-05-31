using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureLvlSelect : MonoBehaviour
{
    public static int pictureNumber = -1;
    // private static bool pictureIsAnimated;
    
    public void UpdatePictureNumber(int number)
    {
        pictureNumber = number;
    }

    public void PlayerLeftPicture()
    {
        UpdatePictureNumber(-1);
    }

    private void Update()
    { 
        Menu.UpdatePictureNumber(pictureNumber);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var tag = gameObject.tag;
            var n = tag[tag.Length - 1];
            UpdatePictureNumber(n);
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        UpdatePictureNumber(-1);
    }
}
