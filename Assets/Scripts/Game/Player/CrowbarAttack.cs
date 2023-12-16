using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarAttack : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private GameObject decalPrefab;
    [SerializeField] private AudioSource crowbarSource;
    private Camera cam;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float decalOffset = 0.01f;
    private bool canAttack = true;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnDisable()
    {
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    void Attack()
    {
        playerAnim.SetTrigger("Attack");

        Ray cameraRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, attackRange, damageableLayer))
        {
            StartCoroutine(CreateDecalDelayed(hit.point, hit.normal, decalOffset, 0.1f));
            IDamage damagable = hit.collider.GetComponent<IDamage>();
            if (damagable != null)
            {
                damagable.TakeDamage(1);
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator CreateDecalDelayed(Vector3 position, Vector3 normal, float offset, float delay)
    {
        yield return new WaitForSeconds(delay);
        position += normal * offset;
        Quaternion rotation = Quaternion.LookRotation(normal) * Quaternion.Euler(0, 180, 0);
        GameObject obj = Instantiate(decalPrefab, position, rotation);
        Destroy(obj, 60f);
        crowbarSource.Play();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
