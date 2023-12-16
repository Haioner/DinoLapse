using UnityEngine;

public class ElevatorScene : MonoBehaviour
{
    private bool once = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && once)
        {
            once = false;
            FindObjectOfType<Transition>().ChangeSceneTo("Lab");
        }
    }
}
