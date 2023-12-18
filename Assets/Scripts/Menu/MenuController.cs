using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    private const string sceneNameKey = "CurrentScene";

    private void Awake()
    {
        if(PlayerPrefs.HasKey(sceneNameKey))
        {
            continueButton.interactable = true;
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey(sceneNameKey);
        FindObjectOfType<Transition>().ChangeSceneTo("RoadScene");
    }

    public void Continue()
    {
        LoaderManager.isToLoad = true;
        FindObjectOfType<Transition>().ChangeSceneTo(PlayerPrefs.GetString(sceneNameKey));
    }

    public void Exit()
    {
        Application.Quit();
    }

}
