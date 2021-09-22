using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public static string prevScene;
    public static string currentScene;
    public Animator transition;
    public float transitionTime = 1f;

    public virtual void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void SwitchScene(string sceneName)
    {
        prevScene = currentScene;
        StartCoroutine(SwitchAnimation());
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator SwitchAnimation()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }
}
