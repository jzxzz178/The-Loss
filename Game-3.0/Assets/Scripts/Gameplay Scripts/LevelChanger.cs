using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private static Animator anim;
    private static int levelToLoad = 4;
    private static readonly int Fade = Animator.StringToHash("fade");

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void ChangeLevelToLoad(int lvl)
    {
        levelToLoad = lvl;
    }

    public static void FadeToLevel()
    {
        anim.SetTrigger(Fade);
    }

    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().name != "Gallery") levelToLoad = 4;
        if (SceneManager.GetActiveScene().name == "Start Menu") levelToLoad = 5;
        if (SceneManager.GetActiveScene().name == "Third Scene") levelToLoad = 6;
        SceneSaver.Save(SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene(levelToLoad);
    }
}