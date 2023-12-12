using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PagesManager : MonoBehaviour
{
    #region Pages Cache
    [Header("Options")]
    public List<GameObject> pages = new List<GameObject>();
    public List<GameObject> pagesButton = new List<GameObject>();

    [Header("State visual")]
    public List<Sprite> spriteButtons = new List<Sprite>();
    public List<Color> colorButtons = new List<Color>();

    private int currentPageIndex = 0;
    #endregion

    private void Awake()
    {
        ChangeButtonVisual();
    }

    #region Pages
    public void PageButtonPress()
    {
        //Get and set button index
        var buttonName = EventSystem.current.currentSelectedGameObject.name;
        for (int i = 0; i < pagesButton.Count; i++)
        {
            if (buttonName == pagesButton[i].name)
            {
                currentPageIndex = i;
            }

        }
        EnableOnePage();
        ChangeButtonVisual();
    }

    void EnableOnePage()
    {
        //Disable all pages
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(false);
        }

        //Enable current page
        pages[currentPageIndex].SetActive(true);
    }

    void ChangeButtonVisual()
    {
        if (spriteButtons.Count != 0)
        {
            for (int i = 0; i < pagesButton.Count; i++)
            {
                if (currentPageIndex == i)
                    pagesButton[i].GetComponent<Image>().sprite = spriteButtons[1];
                else
                    pagesButton[i].GetComponent<Image>().sprite = spriteButtons[0];
            }
        }

        if (colorButtons.Count != 0)
        {
            for (int i = 0; i < pagesButton.Count; i++)
            {
                if (currentPageIndex == i)
                    GetColors(pagesButton[i].GetComponent<Button>(), 1);
                else
                    GetColors(pagesButton[i].GetComponent<Button>(), 0);
            }
        }
    }

    public void GetColors(Button button, int colorValue)
    {
        var colors = button.GetComponent<Button>().colors;
        colors.normalColor = colorButtons[colorValue];
        colors.highlightedColor = colorButtons[colorValue];
        colors.pressedColor = colorButtons[colorValue];
        colors.selectedColor = colorButtons[colorValue];
        button.colors = colors;
    }
    #endregion
}