using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Platform")) //передаем персонажу скорость движущихся платформ
            transform.parent = col.transform;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Platform")) //убираем у персонажа скорость платформы
            transform.parent = null;
    }
}
