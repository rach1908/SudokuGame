using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public GameObject LevelSelectCanvas;
    private GameObject current_canvas;
    private List<GameObject> canvases = new List<GameObject>();

    //To keep track of which menu is selected. If more menus are added, a corresponding option must be added to this Enum aswell
    
    // Start is called before the first frame update
    void Start()
    {
        current_canvas = MainCanvas;
        canvases.Add(TutorialCanvas);
        canvases.Add(MainCanvas);
        canvases.Add(OptionsCanvas);
        canvases.Add(LevelSelectCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMenu_Main()
    {
        if (current_canvas != MainCanvas)
        {
            current_canvas = MainCanvas;
            foreach (GameObject obj in canvases)
            {
                obj.GetComponent<Canvas>().sortingOrder = 0;
            }
            MainCanvas.GetComponent<Canvas>().sortingOrder = 1;
        }
    }
    public void ChangeMenu_Tutorial()
    {
        if (current_canvas != TutorialCanvas)
        {
            current_canvas = TutorialCanvas;
            foreach (GameObject obj in canvases)
            {
                obj.GetComponent<Canvas>().sortingOrder = 0;
            }
            TutorialCanvas.GetComponent<Canvas>().sortingOrder = 1;
        }
    }
    public void ChangeMenu_Options()
    {
        if (current_canvas != OptionsCanvas)
        {
            current_canvas = OptionsCanvas;
            foreach (GameObject obj in canvases)
            {
                obj.GetComponent<Canvas>().sortingOrder = 0;
            }
            OptionsCanvas.GetComponent<Canvas>().sortingOrder = 1;
        }
    }
    public void ChangeMenu_LevelSelect()
    {
        if (current_canvas != LevelSelectCanvas)
        {
            current_canvas = LevelSelectCanvas;
            foreach (GameObject obj in canvases)
            {
                obj.GetComponent<Canvas>().sortingOrder = 0;
            }
            LevelSelectCanvas.GetComponent<Canvas>().sortingOrder = 1;
        }
    }
}
