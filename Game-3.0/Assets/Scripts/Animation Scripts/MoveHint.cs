using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHint : MonoBehaviour
{
    private static Animator anim;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }


    public static void StartAnimation()
    {
        anim.SetTrigger("started moving");
    }
}