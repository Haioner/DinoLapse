using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private List<CarController> carControllers = new List<CarController>();
    [SerializeField] private Transform spawnPos;

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnInterval = 5f;
    [SerializeField] private float maxSpawnInterval = 15f;

    private void Start()
    {
        StartCoroutine(SpawnCarRoutine());
    }

    private IEnumerator SpawnCarRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        if (carControllers.Count > 0 && spawnPos != null)
        {
            // Escolhe um carro aleatório da lista
            CarController randomCar = carControllers[Random.Range(0, carControllers.Count)];

            // Instancia o carro no spawnPos
            Instantiate(randomCar, spawnPos.position, spawnPos.rotation);
        }
        else
        {
            Debug.LogWarning("Car list is empty or spawn position is not set!");
        }
    }
}
