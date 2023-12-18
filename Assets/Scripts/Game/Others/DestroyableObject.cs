using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDamage
{
    [Header("Health")]
    [SerializeField] private float initialHealth;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private AudioSource destroyedSource;
    [SerializeField] private AudioSource hitSource;
    public AudioSource HitAudio
    {
        get { return hitSource; }
        set { hitSource = value; }
    }

    private float currentHealth;
    private bool canDestroy;

    private void Awake()
    {
        currentHealth = initialHealth;
    }

    private void Update()
    {
        InitiateDestroyable();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckDie();
    }

    private void CheckDie()
    {
        if (currentHealth < 0)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            destroyedSource.Play();
            canDestroy = true;


        }
    }

    void InitiateDestroyable()
    {
        if (canDestroy)
        {
            if(TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                renderer.enabled = false;
            }
            if(TryGetComponent<Collider>(out Collider col))
            {
                col.enabled = false;
            }

            if (!destroyedSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

}
