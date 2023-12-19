using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float carSpeed = 5f;

    private void Update()
    {
        MoveForward();
        CheckDestroy();
    }

    private void CheckDestroy()
    {
        if (transform.position.z >= 334 || transform.position.z <= -142)
            Destroy(gameObject);
    }

    private void MoveForward()
    {
        // Move o carro para frente ao longo do eixo Z
        transform.Translate(-Vector3.right * carSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamage damage = other.GetComponent<IDamage>();
            damage?.TakeDamage(100);
        }
    }
}
