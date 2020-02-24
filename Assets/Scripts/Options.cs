using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject color_button01;
    public GameObject color_button02;
    public GameObject color_button03;
    public GameObject error_button;
    //Will need to be updated to load the playerpref once that is a thing
    private error_Highlighting current_error;
    private enum error_Highlighting
    {
        Never,
        Duplicates,
        All
    }
    private enum color_Themes
    {
        Yellow,
        Blue,
        Green
    }
    // Start is called before the first frame update
    void Start()
    {
        current_error = (error_Highlighting)PlayerPrefs.GetInt("error_highlighting");
        color_button01.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Yellow); });
        color_button02.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Blue); });
        color_button03.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Green); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CycleError()
    {
        List<Options.error_Highlighting> valueList = Enum.GetValues(typeof(error_Highlighting)).Cast<Options.error_Highlighting>().ToList();
        int j = valueList.IndexOf(current_error) + 1;
        j = valueList.Count == j ? 0 : j;
        current_error = (error_Highlighting)j;
        PlayerPrefs.SetInt("error_highlighting", j);
        error_button.GetComponentInChildren<Text>().text = current_error.ToString();
        Debug.Log(PlayerPrefs.GetInt("error_highlighting").ToString());
    }

    private void ColorButtons(color_Themes color)
    {
        switch (color)
        {
            case color_Themes.Yellow:
                PlayerPrefs.SetString("c.tile_highlighted", "#EDF50C");
                color_button01.GetComponent<Button>().interactable = false;
                break;
            case color_Themes.Blue:
                PlayerPrefs.SetString("c.tile_highlighted", "#17A4EB");
                color_button02.GetComponent<Button>().interactable = false;
                break;
            case color_Themes.Green:
                PlayerPrefs.SetString("c.tile_highlighted", "#4BC96E");
                color_button03.GetComponent<Button>().interactable = false;
                break;
            default:
                break;
        }
    }
}
