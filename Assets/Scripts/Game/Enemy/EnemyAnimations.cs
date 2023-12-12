using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private List<AudioClip> stepClips = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        MovementAnimation();
    }

    public void PlayRandomStep()
    {
        int randClip = Random.Range(0, stepClips.Count);
        audioSource.PlayOneShot(stepClips[randClip]);
    }

    private void MovementAnimation()
    {
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f)
        {
            anim.SetFloat("speed", speed);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }
    }
}
