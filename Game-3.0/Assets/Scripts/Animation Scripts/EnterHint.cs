using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHint : MonoBehaviour
{
    private static Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public static void StartAppearance()
    {
        anim.SetTrigger("onTrigger");
    }

    public static void StartDisappearance()
    {
        anim.SetTrigger("outTrigger");
    }
}
