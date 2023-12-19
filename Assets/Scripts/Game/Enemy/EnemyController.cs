using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum EnemyState
{
    Patrolling, Chasing
}

public class EnemyController : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyState state;
    private float currentSpeed;

    [Header("Chase")]
    [SerializeField] private float timerToChase = 3f;
    [SerializeField] private float chaseSpeed;
    public bool isTrigger;
    private float currentTimerChasing;

    [Header("Patrol")]
    [SerializeField] private float randomPatrolRange = 20f;
    [SerializeField] private float cooldownNextPoint = 5f;
    [SerializeField] private float patrolSpeed;
    private float currentCooldownNextPoint;

    [Header("Audios")]
    [SerializeField] private AudioSource roarSource;
    [SerializeField] private AudioSource chaseMusic;
    private bool canRoar = true;
    private float initialChaseVolume;

    private void Start()
    {
        initialChaseVolume = chaseMusic.volume;
    }

    private void Update()
    {
        CalculateState();
        Chasing();
        CalculatePatrolling();
    }

    public EnemyState GetCurrentState()
    {
        return state;
    }

    private void CalculateState()
    {
        if (isTrigger)
        {
            currentTimerChasing = timerToChase;

        }

        if(currentTimerChasing > 0)
        {
            //Chase
            currentTimerChasing -= Time.deltaTime;
            currentSpeed = chaseSpeed;
            state = EnemyState.Chasing;
            PlayRoarAudio();
        }
        else
        {
            //Patrolling
            currentTimerChasing = 0;
            currentSpeed = patrolSpeed;
            state = EnemyState.Patrolling;
            canRoar = true;
        }

        agent.speed = currentSpeed;
    }

    private void PlayRoarAudio()
    {
        if (canRoar && !roarSource.isPlaying)
        {
            roarSource.Play();
            canRoar = false;
        }
    }

    private void Chasing()
    {
        if (state == EnemyState.Chasing)
        {
            agent.SetDestination(target.position);
            PlayChaseMusic(true);
        }
        else
            PlayChaseMusic(false);
    }

    public void SetChaseMusicPitch(float pitchValue)
    {
        chaseMusic.pitch = pitchValue;
    }

    private void PlayChaseMusic(bool stateMusic)
    {
        switch (stateMusic)
        {
            case true:
                StopCoroutine(FadeOutChaseMusic());
                chaseMusic.volume = initialChaseVolume;
                if (!chaseMusic.isPlaying)
                    chaseMusic.Play();
                break;
            case false:
                if (chaseMusic.isPlaying)
                    StartCoroutine(FadeOutChaseMusic());
                break;
        }
    }

    private IEnumerator FadeOutChaseMusic()
    {
        float initialVolume = chaseMusic.volume;
        float elapsedTime = 0f;
        float fadeDuration = 2f;

        while (elapsedTime < fadeDuration)
        {
            chaseMusic.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        chaseMusic.Stop();
        chaseMusic.volume = initialVolume;
    }

    private void CalculatePatrolling()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentCooldownNextPoint > 0)
                currentCooldownNextPoint -= Time.deltaTime;
            else
            {
                Vector3 point;
                if (RandomPoint(transform.position, randomPatrolRange, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                    currentCooldownNextPoint = cooldownNextPoint;
                }
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
