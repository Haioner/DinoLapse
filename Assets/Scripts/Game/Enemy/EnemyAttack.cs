using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private AudioSource attackSource;

    private bool canAttack = true;

    private void Update()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider hitCollider in hitColliders)
        {
            IDamage damagable = hitCollider.GetComponent<IDamage>();
            if (damagable != null && hitCollider.CompareTag("Player"))
            {
                damagable.TakeDamage(1);
                StartCoroutine(AttackCooldown());
                anim.SetTrigger("Attack");
                canAttack = false;
                attackSource.Play();
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
