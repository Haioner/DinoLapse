using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

public class KeyController : MonoBehaviour, IInteractable
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

    private void Awake() => audioSource = GetComponent<AudioSource>();

    public void Interact()
    {
        door.SetDoorActive(true);
        audioSource.Play();
        StartCoroutine(DestroyOnAudioEnd());
    }

    private IEnumerator DestroyOnAudioEnd()
    {
        while (audioSource.isPlaying)
            yield return null;
        
        Destroy(gameObject);
    }
}
