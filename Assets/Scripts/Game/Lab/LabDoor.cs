using UnityEngine;

public class LabDoor : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource doorSource;
    private bool isOpened;

    [Header("Open")]
    [SerializeField] private bool canOpen;
    [SerializeField] private bool openOnce;
    [SerializeField] private Collider openCollider;

    [Header("Close")]
    [SerializeField] private bool canClose;
    [SerializeField] private bool closeOnce;
    [SerializeField] private Collider closeCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened && canClose)
        {
            CloseDoor();

            if (closeOnce)
            {
                canClose = false;
                closeCollider.enabled = false;
            }
            
        }
        else if (canOpen)
        {
            OpenDoor();

            if (openOnce)
            {
                canOpen = false;
                openCollider.enabled = false;

            }
        }

    }

    public void OpenDoor()
    {
        anim.Play("Open");
        doorSource.Play();
        isOpened = true;
    }

    public void CloseDoor()
    {
        anim.Play("Close");
        doorSource.Play();
        isOpened = false;
    }
}
