using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

public class FlashLightBattery : MonoBehaviour, IInteractable
{
    [Header("Interact")]
    public LocalizedString interactMessage;
    public LocalizedString fullBatteryMessage;
    public LocalizedString InteractMessage
    {
        get { return interactMessage; }
        set { interactMessage = value; }
    }
    private LocalizedString initialInteractMessage;

    [Space]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int batteryCharge = 5;
    private bool _canInteract = true;

    private void Awake() => initialInteractMessage = interactMessage;

    public void Interact()
    {
        if (!_canInteract) return;
        Flashlight_controller flashLight = FindAnyObjectByType<Flashlight_controller>();
        if (flashLight.AddBattery(batteryCharge))
        {
            audioSource.Play();
            _canInteract = false;
            StartCoroutine(DestroyOnAudioEnd());
        }
        else
        {
            StartCoroutine(UpdateInteractMessage());
        }
    }

    private IEnumerator UpdateInteractMessage()
    {
        interactMessage = fullBatteryMessage;
        yield return new WaitForSeconds(1);
        interactMessage = initialInteractMessage;
    }

    private IEnumerator DestroyOnAudioEnd()
    {
        while (audioSource.isPlaying)
            yield return null;

        Destroy(gameObject);
    }
}
