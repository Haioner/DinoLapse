using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private LocalizedString interactMessage;

    [Header("FeedBack")]
    [SerializeField] private CanvasGroup interactCanvas;

    private Camera _playerCamera;
    private RaycastHit hit;
    private DefaultInput _defaultInput;

    private void Awake()
    {
        _defaultInput = new DefaultInput();
        _defaultInput.Interactables.InteractButton.performed += e => HandleInteraction();
        _defaultInput.Enable();

        _playerCamera = Camera.main;
    }

    private void Update() => FeedBack();

    private void OnDisable() => interactCanvas.alpha = 0;

    private void FeedBack()
    {
        if (GetInteractObject() == null)
        {
            interactCanvas.alpha = 0;
            return;
        }

        if (GetInteractObject().TryGetComponent<IInteractable>(out IInteractable interactable)
            ||
            GetInteractObject().layer == LayerMask.NameToLayer("Interactable"))
        {
            interactCanvas.alpha = 1;

            if (interactable != null)
            {
                //Has found interface
                interactable.InteractMessage.GetLocalizedStringAsync().Completed += handle =>
                {
                    string translatedText = handle.Result;
                    interactText.text = translatedText;
                };

            }
            else
            {
                //Hasnt found interface
                interactMessage.GetLocalizedStringAsync().Completed += handle =>
                {
                    string translatedText = handle.Result;
                    interactText.text = translatedText;
                };
            }
        }
        else
            interactCanvas.alpha = 0;
    }

    private void HandleInteraction()
    {
        if (!enabled) return;

        if(GetInteractObject() != null && GetInteractObject().TryGetComponent<IInteractable>(out IInteractable interactable))
            interactable.Interact();
    }

    private GameObject GetInteractObject()
    {
        Ray ray = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        int layerMask = 1 << LayerMask.NameToLayer("Player");

        if (Physics.Raycast(ray, out hit, interactDistance, ~layerMask))
            return hit.collider.gameObject;
        else
            return null;
    }
}