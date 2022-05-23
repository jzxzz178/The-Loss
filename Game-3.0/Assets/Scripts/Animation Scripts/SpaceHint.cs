using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHint : MonoBehaviour
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
