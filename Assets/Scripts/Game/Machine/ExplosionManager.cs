using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float minTimeBetweenExplosions = 1f;
    [SerializeField] private float maxTimeBetweenExplosions = 5f;
    [SerializeField] private Animator cameraAnimator;

    void Start()
    {
        StartCoroutine(GenerateExplosions());
    }

    IEnumerator GenerateExplosions()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenExplosions, maxTimeBetweenExplosions));

            Vector3 randomDirection = Random.onUnitSphere;
            float randomDistance = Random.Range(minDistance, maxDistance);

            Vector3 randomPosition = transform.position + randomDirection * randomDistance;

            Instantiate(explosionParticle, randomPosition, Quaternion.identity);
            cameraAnimator.SetTrigger("Shake");
        }
    }
}
