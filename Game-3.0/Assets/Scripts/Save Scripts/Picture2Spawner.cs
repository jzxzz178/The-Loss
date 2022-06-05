using UnityEngine;

public class Picture2Spawner : MonoBehaviour
{
    private bool flag;

    private void Update()
    {
        if (SceneSaver.CheckForSave("First Scene") && !flag)
        {
            transform.position = new Vector3(-0.14f, 2.94f - 0.88f, 8.8f);
            flag = true;
        }
    }
}