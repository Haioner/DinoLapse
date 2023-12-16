using UnityEngine.Localization;
using UnityEngine;

public interface IInteractable
{
    public void Interact() { }

    //public string InteractMessage { get; set; }
    public LocalizedString InteractMessage { get; set; }
}
