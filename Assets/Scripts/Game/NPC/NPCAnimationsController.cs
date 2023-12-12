using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationsController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;

    private void Update()
    {
        MovementAnimation();
    }

    private void MovementAnimation()
    {
        float speed = agent.velocity.magnitude;
        if(speed > 0.1f)
        {
            anim.SetFloat("speed", speed);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }
    }
}
