using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MachineManager : MonoBehaviour
{
    [Header("Center Machine")]
    [SerializeField] private Transform centerMachine;
    [SerializeField] private float speedRotation;
    [SerializeField] private AudioSource centerSource;
    [SerializeField] private GameObject vitrineLights;
    private int currentMachines;

    [Header("Machine Timeline")]
    [SerializeField] private PlayableDirector director;
    private bool hasStartedDestroyed;
    private float charge;

    private void Update()
    {
        RotateMachines();
        ChargeMachine();
    }

    private void RotateMachines()
    {
        centerMachine.Rotate((currentMachines * speedRotation) * Time.deltaTime, 0, 0);
    }

    public void AddMachine()
    {
        currentMachines++;
        CheckMachines();
    }

    private void CheckMachines()
    {
        if (currentMachines >= 2)
        {
            centerSource.Play();
            vitrineLights.SetActive(true);
        }
    }

    private void ChargeMachine()
    {
        if (hasStartedDestroyed) return;

        if(currentMachines>= 2)
        {
            charge += 2 * Time.deltaTime;
        }

        if(charge > 10)
        {
            director.Play();
            hasStartedDestroyed = true;
        }
    }
}
