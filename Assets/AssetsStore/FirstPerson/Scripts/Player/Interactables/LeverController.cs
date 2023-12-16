using UnityEngine;
using UnityEngine.Localization;

public class LeverController : MonoBehaviour, IInteractable
{
    [Header("Interact")]
    public LocalizedString interactMessage;
    public LocalizedString InteractMessage
    {
        get { return interactMessage; }
        set { interactMessage = value; }
    }

    [Space]
    [SerializeField] private DoorController door;
    private AudioSource audioSource;
    private Animator anim;
    private bool _leverActive = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        audioSource.Play();
        _leverActive = !_leverActive;
        anim.SetBool("Lever", _leverActive);

        door.SetDoorActive(true);
        door.Interact();
        door.SetDoorActive(false);
    }
}
