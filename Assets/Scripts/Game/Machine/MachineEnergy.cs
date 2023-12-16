using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineEnergy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private MachineManager machineManager;
    [SerializeField] private GameObject lightningParticle;

    [Header("Rotation")]
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private float speedRotation = 100f;
    private bool canRotate;

    [Header("Audios")]
    [SerializeField] private AudioSource machineMovement;
    [SerializeField] private AudioSource machineLoopRotation;

    private void Update()
    {
        if (canRotate)
            rotateTransform.Rotate(0, speedRotation * Time.deltaTime, 0);
    }

    public void EnableMachine()
    {
        anim.Play("On");
        machineMovement.Play();
    }

    public void FinishedAnimation()
    {
        lightningParticle.SetActive(true);
        machineManager.AddMachine();
        anim.enabled = false;
        machineLoopRotation.Play();
        canRotate = true;
    }
}
