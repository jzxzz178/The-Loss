using UnityEngine;

public class Picture3Spawner : MonoBehaviour
{
    private bool flag;

    private void Update()
    {
        if (PlayerPrefs.HasKey("Second Scene") && !flag)
        {
            transform.position = new Vector3(4.02f, 2.94f - 0.88f, 8.8f);
            flag = true;
        }
    }
}