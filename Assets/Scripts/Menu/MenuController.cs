using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        FindObjectOfType<Transition>().ChangeSceneTo("RoadScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
