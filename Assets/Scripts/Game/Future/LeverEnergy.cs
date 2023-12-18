using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class LeverEnergy : MonoBehaviour, IInteractable
{
    [Header("Interact")]
    public LocalizedString interactMessage;
    public LocalizedString InteractMessage
    {
        get { return interactMessage; }
        set { interactMessage = value; }
    }

    [Space]
    private AudioSource audioSource;
    private Animator anim;
    private bool _leverActive = false;
    private bool canInteract = true;
    [SerializeField] private PowerManager powerManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (canInteract)
        {
            canInteract = false;
            audioSource.Play();
            _leverActive = !_leverActive;
            anim.SetBool("Lever", _leverActive);
            powerManager.EnablePower();
        }
    }
}
