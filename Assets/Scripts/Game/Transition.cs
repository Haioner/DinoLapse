using UnityEngine.SceneManagement;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator anim;
    private string sceneName;

    public void ChangeSceneTo(string _sceneName)
    {
        //Play Off transition and set the scene name
        anim.Play("outTransition");
        sceneName = _sceneName;
    }

    public void ChangeInEvent()
    {
        //Change scene (in event annimation)
        SceneManager.LoadScene(sceneName);
    }
}