using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPosition : MonoBehaviour
{
    public GameObject Main_Canvas;
    public GameObject Tutorial_Canvas;
    public GameObject Options_Canvas;
    public GameObject Level_Canvas;
    public GameObject Button_Main;
    // Start is called before the first frame update
    void Start()
    {
        MainSetup();
        TutorialSetup();
        LevelSetup();
    }


    public void MainSetup()
    {
        //The Image should take up 2 thirds of the vertical space, and the 3 buttons should be in the middle of the remaining portion
        Main_Canvas.GetComponentInChildren<Image>().GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / 2, Screen.height / 3 * 2);
        Button[] buttons = Main_Canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / 4 * (i + 1), Screen.height / 3);
        }
    }

    public void TutorialSetup()
    {
        Tutorial_Canvas.GetComponentInChildren<Text>().GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / 2, Screen.height / 5 * 4);
    }

    public void OptionSetup()
    {
        //Header text should be the first object in the OptionsCanvas
        Options_Canvas.GetComponentInChildren<Text>().GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / 2, Screen.height / 6 * 5);
        Button[] buttons = Options_Canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 != buttons.Length)
            {
                //??+
            }
        }
    }

    public void LevelSetup()
    {
        Button[] buttons = Level_Canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / 7 * (i == 0 ? 1 : 6), Screen.height / 8);
        }
    }
}
