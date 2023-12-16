using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class FlashLightItem : MonoBehaviour, IInteractable
{
    [Header("Interact")]
    public LocalizedString interactMessage;
    public LocalizedString InteractMessage
    {
        get { return interactMessage; }
        set { interactMessage = value; }
    }

    public void Interact()
    {
        FindObjectOfType<PlayerHandManager>().EnableFlashLight();
        Destroy(gameObject);
    }

}
