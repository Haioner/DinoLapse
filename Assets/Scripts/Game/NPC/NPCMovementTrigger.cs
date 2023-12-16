using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementTrigger : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] private bool canStartMove;
    [SerializeField] private bool loop;
    [SerializeField] private float cooldownToMove;
    private bool npcTriggered = false;

    private void Start()
    {
        if(canStartMove)
            StartCoroutine(MoveWithCooldown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !npcTriggered && !canStartMove) 
        {
            if (!loop)
                npcTriggered = true;

            StartCoroutine(MoveWithCooldown());
        }
    }

    private IEnumerator MoveWithCooldown()
    {
        while (true)
        {
            foreach (Transform target in targets)
            {
                agent.SetDestination(target.position);
                yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.1f);

                yield return new WaitForSeconds(cooldownToMove);
            }

            if (!loop)
                break;
        }
    }
}
