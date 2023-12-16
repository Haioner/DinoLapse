using UnityEngine;

public class LightAlert : MonoBehaviour
{
    [SerializeField] private Light alertLight;
    [SerializeField] private float cycleDuration = 2f; // Duração de um ciclo em segundos
    private float initialIntensitie;

    private void Awake()
    {
        initialIntensitie = alertLight.intensity;
    }

    void Update()
    {
        // Calcula o valor do seno baseado no tempo atual e na duração do ciclo
        float intensity = Mathf.Lerp(0f, initialIntensitie, Mathf.Sin(2 * Mathf.PI * Time.time / cycleDuration));

        // Aplica o valor do seno à intensidade da luz
        alertLight.intensity = intensity;
    }
}
