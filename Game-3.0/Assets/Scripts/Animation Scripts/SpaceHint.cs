using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHint : MonoBehaviour
{
    private static Animator anim;
    private static bool flag;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        flag = false;
    }


    public static void StartAnimation()
    {
        if (flag) return;
        anim.SetTrigger("started moving");
        flag = true;
    }
}
