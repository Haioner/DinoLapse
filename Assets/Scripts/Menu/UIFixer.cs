using UnityEngine.Events;
using UnityEngine;

public class UIFixer : MonoBehaviour
{
    [SerializeField] private UnityEvent fixerEvent;

    private void Start()
    {
        fixerEvent?.Invoke();
    }
}
