using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour
{
    [SerializeField] private CanvasGroup CG;
    [SerializeField] private KeyCode inputKey = KeyCode.Escape;
    private bool currentState = false;

    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
            SwichState();
    }

    public void SwichState()
    {
        currentState = !currentState;
        CinemachineState(!currentState);
        MouseState();
        if (!currentState)
        {
            CG.alpha = 0;
            CG.interactable = false;
            CG.blocksRaycasts = false;
            TimeGameState(1);
        }
        else
        {
            CG.alpha = 1;
            CG.interactable = true;
            CG.blocksRaycasts = true;
            TimeGameState(0);
        }
    }

    private void TimeGameState(float time)
    {
        Time.timeScale = time;
    }

    private void MouseState()
    {
        if (!currentState)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private void CinemachineState(bool state)
    {
        //FindObjectOfType<MouseSensitivityManager>().cvc.enabled = state;
    }
}
