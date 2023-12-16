using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class KittyController : MonoBehaviour, IInteractable
{
    [Header("Interact")]
    public LocalizedString interactMessage;
    public LocalizedString InteractMessage
    {
        get { return interactMessage; }
        set { interactMessage = value; }
    }

    [Header("CACHE")]
    [SerializeField] private Animator anim;
    [SerializeField] private List<AudioSource> sources = new List<AudioSource>();

    public void Interact()
    {
        anim.SetTrigger("Cuddle");

        for (int i = 0; i < sources.Count; i++)
        {
            if (!sources[i].isPlaying)
                sources[i].Play();
        }
    }
}
