using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EqquipedItem
{
   hand, flashlight, crowbar
}

public class PlayerHandManager : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private EqquipedItem eqquipedItem;
    [SerializeField] private AudioSource itemSource;
    [SerializeField] private float animWeightLerpSpeed = 50f;

    [Header("Crowbar")]
    [SerializeField] private bool canCrowbar;
    [SerializeField] private GameObject crowbar;

    [Header("FlashLight")]
    [SerializeField] private bool canFlashlight;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject flashLightShoulder;
    [SerializeField] private GameObject flashLightCanvas;
    [SerializeField] private HandCameraDirection handCameraDirection;
    public bool isFlashing;

    private float targetAnimWeight = 0f;
    private float currentAnimWeight = 0f;

    private void Update()
    {
        SelectItem();
        SetAnimWeight(targetAnimWeight);
    }

    public void EnableCrowbar()
    {
        canCrowbar = true;
        eqquipedItem = EqquipedItem.crowbar;
        UpdateItem();
    }

    public void EnableFlashLight()
    {
        isFlashing = true;
        canFlashlight = true;
        eqquipedItem = EqquipedItem.flashlight;
        UpdateItem();
    }

    private void SelectItem()
    {
        // Escolha de item usando as teclas numéricas
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            eqquipedItem = EqquipedItem.hand;
            UpdateItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && canFlashlight)
        {
            eqquipedItem = EqquipedItem.flashlight;
            UpdateItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && canCrowbar)
        {
            eqquipedItem = EqquipedItem.crowbar;
            UpdateItem();
        }

        // Escolha de item usando a roda do mouse
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll > 0f)
        {
            // Rotação positiva da roda do mouse (para cima)
            CycleSelectedItem(1);
        }
        else if (mouseScroll < 0f)
        {
            // Rotação negativa da roda do mouse (para baixo)
            CycleSelectedItem(-1);
        }
    }

    private void CycleSelectedItem(int direction)
    {
        // Lógica para ciclar entre os itens usando a roda do mouse
        int itemCount = System.Enum.GetValues(typeof(EqquipedItem)).Length;
        int currentIndex = (int)eqquipedItem;
        int newIndex = (currentIndex + direction + itemCount) % itemCount;
        eqquipedItem = (EqquipedItem)newIndex;

        UpdateItem();
    }

    private void UpdateItem()
    {
        switch (eqquipedItem)
        {
            case EqquipedItem.hand:
                ActiveItem(null);
                SetTargetAnimWeight(0);
                break;

            case EqquipedItem.flashlight:
                if (!canFlashlight) return;

                ActiveItem(flashLight);
                SetTargetAnimWeight(1);
                SelectAnimBool("IsFlashLight");
                break;

            case EqquipedItem.crowbar:
                if (!canCrowbar) return;

                CrowbarAndFlashLightShoulder();
                //ActiveItem(crowbar);
                SetTargetAnimWeight(1);
                SelectAnimBool("IsCrowbar");
                break;

            default:
                SetTargetAnimWeight(0);
                ActiveItem(null);
                break;
        }

        UpdateFlashLightElementsActive();
        itemSource.Play();
    }

    private void SetTargetAnimWeight(float targetWeight)
    {
        targetAnimWeight = targetWeight;
    }

    private void SetAnimWeight(float targetWeight)
    {
        currentAnimWeight = Mathf.Lerp(currentAnimWeight, targetWeight, animWeightLerpSpeed * Time.deltaTime);
        playerAnim.SetLayerWeight(1, currentAnimWeight);
    }

    private void UpdateFlashLightElementsActive()
    {
        handCameraDirection.enabled = flashLight.activeInHierarchy;
        //flashLightCanvas.SetActive(isFlashing);
    }


    private void CrowbarAndFlashLightShoulder()
    {
        if (isFlashing)
        {
            ActiveItem(flashLightShoulder);
            crowbar.SetActive(true);
        }
        else
        {
            ActiveItem(crowbar);
        }
    }

    private void ActiveItem(GameObject item)
    {
        crowbar.SetActive(false);
        flashLight.SetActive(false);
        flashLightShoulder.SetActive(false);

        if (item != null)
            item.SetActive(true);
    }

    private void SelectAnimBool(string boolName)
    {
        playerAnim.SetBool("IsFlashLight", false);
        playerAnim.SetBool("IsCrowbar", false);

        playerAnim.SetBool(boolName, true);
    }
}
