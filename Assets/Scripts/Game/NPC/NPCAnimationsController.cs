using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AnimationsNPC
{
    none, sit, button, fax, type, terrified
}

public class NPCAnimationsController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationsNPC overwriteAnimation;

    private void Start()
    {
        if (overwriteAnimation == AnimationsNPC.none) return;
        OverwriteAnimation();
    }

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

    private void OverwriteAnimation()
    {
        switch (overwriteAnimation)
        {
            case AnimationsNPC.sit: 
                anim.Play("Sit-Hu");
                break;

            case AnimationsNPC.button:
                anim.Play("ButtonPushing");
                break;

            case AnimationsNPC.fax:
                anim.Play("SendingFax");
                break;

            case AnimationsNPC.type:
                anim.Play("Typing");
                break;

            case AnimationsNPC.terrified:
                anim.Play("Terrified");
                break;
        }
    }
}
