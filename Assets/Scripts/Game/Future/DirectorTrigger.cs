using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    private bool interactOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && interactOnce)
        {
            director.Play();
            interactOnce = false;
        }
    }
}
