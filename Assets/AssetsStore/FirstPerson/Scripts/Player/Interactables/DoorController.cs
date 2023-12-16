using UnityEngine;
using UnityEngine.Localization;

public class DoorController : MonoBehaviour, IInteractable
{
    [Header("Door Interaction")]
    [SerializeField] private bool canInteract = true;
    public LocalizedString interactMessage;
    public LocalizedString lockedMessage;
    public LocalizedString InteractMessage
    {
        get 
        {
            if (canInteract)
                return interactMessage;
            else
                return lockedMessage;
        }
        set { interactMessage = value; }
    }

    [Header("Door Sounds")]
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip lockedClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private float delayCloseSound;

    private Animator anim;
    private AudioSource _audioSource;
    private bool _doorOpened = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void SetDoorActive(bool state)
    {
        canInteract = state;
    }

    public void Interact()
    {
        PlaySounds();

        if (canInteract)
            DoorAnimations();
    }

    private void DoorAnimations()
    {
        _doorOpened = !_doorOpened;
        anim.SetBool("Door", _doorOpened);
    }

    private void PlaySounds()
    {
        if (!canInteract)
        {
            _audioSource.PlayOneShot(lockedClip);
            return;
        }

        if (_doorOpened)
        {
            _audioSource.clip = closeClip;
            _audioSource.PlayDelayed(delayCloseSound);
        }
        else
        {
            _audioSource.PlayOneShot(openClip);
        }
    }
}
