using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyTo : MonoBehaviour
{
    [SerializeField] private Transform posToMove;
    EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void MoveTo_EVENT()
    {
        if (!enemyController.isTrigger && transform.position.z > -20f)
        {
            enemyController.SetDestination(posToMove);
        }
    }
}
