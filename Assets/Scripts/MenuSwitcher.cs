using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public GameObject LevelSelectCanvas;
    public GameObject button_lvlselect;
    public GameObject button_options;
    public GameObject button_HowToPlay;
    public GameObject button_htpToMain;
    public GameObject button_optToMain;
    public GameObject button_lvlToMain;
    private menus current_menu;
    private List<Canvas> all_menus = new List<Canvas>();

    //To keep track of which menu is selected. If more menus are added, a corresponding option must be added to this Enum aswell
    
    // Start is called before the first frame update
    void Start()
    {
        current_menu = menus.Main;
        all_menus.AddRange(new List<Canvas> {
            TutorialCanvas.GetComponent<Canvas>(),
            MainCanvas.GetComponent<Canvas>(),
            OptionsCanvas.GetComponent<Canvas>(),
            LevelSelectCanvas.GetComponent<Canvas>()
        });
        //Assigning a listener to each button that changes the menu page
        //If more menu pages are added, they must have a corresponding Gameobject above, and listener below
        //as well as an option in the 'menus' enum, the all_menus list, and the switch in ChangeMenu()
        button_lvlselect.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.LevelSelect); });
        button_options.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.Options); });
        button_HowToPlay.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.HowToPlay); });
        button_htpToMain.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.Main); });
        button_optToMain.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.Main); });
        button_lvlToMain.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.Main); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private enum menus
    {
        Main,
        HowToPlay,
        Options,
        LevelSelect
    }
    private void ChangeMenu(menus menu)
    {
        if (current_menu != menu)
        {
            foreach (Canvas canvas in all_menus)
            {
                canvas.GetComponent<Canvas>().sortingOrder = 0;
            }
            switch (menu)
            {
                case menus.Main:
                    MainCanvas.GetComponent<Canvas>().sortingOrder = 1;
                    break;
                case menus.HowToPlay:
                    TutorialCanvas.GetComponent<Canvas>().sortingOrder = 1;
                    break;
                case menus.Options:
                    OptionsCanvas.GetComponent<Canvas>().sortingOrder = 1;
                    break;
                case menus.LevelSelect:
                    LevelSelectCanvas.GetComponent<Canvas>().sortingOrder = 1;
                    break;
                default:
                    Debug.Log("An option in the 'menus' enum does not have a corresponding option in the switch in ChangeMenu() - MenuSwitcher.cs");
                    break;
            }
            current_menu = menu;
        }
    }
}
