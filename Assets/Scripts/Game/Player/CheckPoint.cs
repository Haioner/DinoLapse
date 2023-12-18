using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private const string playerPositionKey = "PlayerPosition";
    private const string sceneNameKey = "CurrentScene";
    private const string crowbarKey = "CanCrowBar";
    private const string flashLightKey = "CanFlashLight";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHandManager playerHand = other.GetComponent<PlayerHandManager>();
            SaveHand(playerHand);
            SaveLocation(other.transform.position, SceneManager.GetActiveScene().name);
        }
    }

    private void SaveLocation(Vector3 playerPosition, string sceneName)
    {
        //Location
        PlayerPrefs.SetFloat($"{playerPositionKey}_X", playerPosition.x);
        PlayerPrefs.SetFloat($"{playerPositionKey}_Y", playerPosition.y);
        PlayerPrefs.SetFloat($"{playerPositionKey}_Z", playerPosition.z);

        //Scene
        PlayerPrefs.SetString(sceneNameKey, sceneName);

        PlayerPrefs.Save();
        Debug.Log("Saved location");
    }

    private void SaveHand(PlayerHandManager playerHandManager)
    {
        if (playerHandManager.GetCanCrowbar())
            PlayerPrefs.SetFloat(crowbarKey, 1);
        else
            PlayerPrefs.SetFloat(crowbarKey, 0);

        if (playerHandManager.GetCanFlashLight())
            PlayerPrefs.SetFloat(flashLightKey, 1);
        else
            PlayerPrefs.SetFloat(flashLightKey, 0);

        PlayerPrefs.Save();
        Debug.Log("Saved hand");
    }
}
