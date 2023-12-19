using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private float currentHealth;
    private float initialHealth;

    [Header("CACHE")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator damageAnim;
    [SerializeField] private AudioSource damageSource;
    [SerializeField] private AudioSource dieSource;
    [SerializeField] private AudioSource hitSource;
    public AudioSource HitAudio
    {
        get { return hitSource; }
        set { hitSource = value; }
    }

    private void Awake()
    {
        initialHealth = maxHealth;
        currentHealth = initialHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damageValue)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damageValue;
        damageAnim.SetTrigger("Damage");
        damageSource.Play();
        CheckLife();
    }

    private void CheckLife()
    {
        if(currentHealth <= 0)
        {
            playerController.SetMoveState(false);
            playerController.SetViewState(false);
            playerAnim.enabled = true;
            playerAnim.SetTrigger("Die");
            dieSource.Play();
        }
    }

    public void DeathEvent()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        FindObjectOfType<Transition>().ChangeSceneTo(currentSceneName);
    }
}
