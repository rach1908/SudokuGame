using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject Master_canvas;
    public GameObject TutorialCanvas;
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public GameObject LevelSelectCanvas;
    public GameObject button_lvlselect;
    public GameObject button_options;
    public GameObject button_HowToPlay;
    public GameObject button_main;
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
        button_main.GetComponent<Button>().onClick.AddListener(delegate { ChangeMenu(menus.Main); });
        //Setting all but the main canvas to unactive
        TutorialCanvas.gameObject.SetActive(false);
        OptionsCanvas.gameObject.SetActive(false);
        LevelSelectCanvas.gameObject.SetActive(false);
        button_main.gameObject.SetActive(false);
        button_main.GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / 2 - (button_main.GetComponent<RectTransform>().rect.width / 2 + 25), Screen.height / 2 - (button_main.GetComponent<RectTransform>().rect.height / 2 + 25));
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
                canvas.gameObject.SetActive(false);
            }
            switch (menu)
            {
                case menus.Main:
                    MainCanvas.gameObject.SetActive(true);
                    button_main.gameObject.SetActive(false);
                    break;
                case menus.HowToPlay:
                    TutorialCanvas.gameObject.SetActive(true);
                    button_main.gameObject.SetActive(true);
                    break;
                case menus.Options:
                    OptionsCanvas.gameObject.SetActive(true);
                    button_main.gameObject.SetActive(true);
                    break;
                case menus.LevelSelect:
                    LevelSelectCanvas.gameObject.SetActive(true);
                    button_main.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("An option in the 'menus' enum does not have a corresponding option in the switch in ChangeMenu() - MenuSwitcher.cs");
                    break;
            }
            current_menu = menu;
        }
    }
}
