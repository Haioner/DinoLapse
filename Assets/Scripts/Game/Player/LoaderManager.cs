using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{
    //[SerializeField] private bool CanAwakeLoad = false;
    public static bool isToLoad;
    private const string playerPositionKey = "PlayerPosition";
    private const string sceneNameKey = "CurrentScene";
    private const string crowbarKey = "CanCrowBar";
    private const string flashLightKey = "CanFlashLight";

    void Awake()
    {
        if (isToLoad)
        {
            LoadPlayer();
            isToLoad = false;
        }
    }

    private void LoadPlayer()
    {
        if (PlayerPrefs.HasKey(sceneNameKey))
        {
            string loadedSceneName = PlayerPrefs.GetString(sceneNameKey);

            // Verifique se a cena salva é a mesma da cena atual
            if (loadedSceneName == SceneManager.GetActiveScene().name)
            {
                Vector3 loadedPlayerPosition = new Vector3(
                    PlayerPrefs.GetFloat($"{playerPositionKey}_X"),
                    PlayerPrefs.GetFloat($"{playerPositionKey}_Y"),
                    PlayerPrefs.GetFloat($"{playerPositionKey}_Z")
                );

                // Mova o jogador para a posição salva
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.GetComponent<PlayerController>().SetMoveState(false);
                    player.transform.position = loadedPlayerPosition;
                    StartCoroutine(DelayToEnablePlayer(player));

                    // Carregue o estado da mão do jogador (Crowbar, Flashlight)
                    PlayerHandManager playerHandManager = player.GetComponent<PlayerHandManager>();
                    if (playerHandManager != null)
                    {
                        bool canCrowbar = PlayerPrefs.GetFloat(crowbarKey) == 1;
                        bool canFlashLight = PlayerPrefs.GetFloat(flashLightKey) == 1;

                        if (canCrowbar)
                            playerHandManager.EnableCrowbar();
                        
                        if(canFlashLight)
                            playerHandManager.EnableFlashLight();
                    }

                    Debug.Log("Jogador carregado com sucesso!");
                }
                else
                {
                    Debug.LogWarning("Objeto do jogador não encontrado!");
                }
            }
            else
            {
                Debug.LogWarning("A cena salva não corresponde à cena atual!");
                SceneManager.LoadScene(PlayerPrefs.GetString(sceneNameKey));
            }
        }
        else
        {
            Debug.Log("Nenhum dado de salvamento encontrado.");
        }
    }

    private IEnumerator DelayToEnablePlayer(GameObject player)
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().SetMoveState(true);
    }
}

