using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent powerEvents;
    [SerializeField] private List<Light> lights = new List<Light>();

    [ContextMenu("Enable Power")]
    public void EnablePower()
    {
        powerEvents?.Invoke();
        SetLightsOn();
    }

    private void SetLightsOn()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].enabled = true;
            lights[i].color = Color.white;
            if (lights[i].GetComponentInParent<LightAlert>() != null)
                lights[i].GetComponentInParent<LightAlert>().enabled = false;

            lights[i].intensity = 4;
        }
    }
}
