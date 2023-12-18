using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTrigger : MonoBehaviour
{
    [SerializeField] private MachineEnergy machineEnergy;
    [SerializeField] private AudioSource batterySource;
    [SerializeField] private GameObject batteryObj;
    [SerializeField] private bool canCharge = true;
    private bool hasPut;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Battery" && !hasPut && canCharge)
        {
            hasPut = true;
            batterySource.Play();
            batteryObj.SetActive(true);
            other.gameObject.SetActive(false);
            FindAnyObjectByType<Grab_Items>().RemoveGrab();
            machineEnergy.EnableMachine();
        }
    }

    public void EnableCanCharge()
    {
        canCharge = true;
    }
}
