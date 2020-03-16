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
    private error_Highlighting current_error;
    private List<Button> color_buttons = new List<Button>();
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
    //All playerPrefs keys - be sure to call playerprefs using this enum to avoid spelling errors
    public enum pref_keys
    {
        c_tile_default_str,
        c_tile_highlighted_str,
        c_text_given_str,
        c_text_input_str,
        c_error_str,
        error_highlighting_int,
        level_page_all_int,
        level_page_easy_int,
        level_page_medium_int,
        level_page_hard_int,
        selected_difficulty_int
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetString(pref_keys.c_tile_highlighted_str.ToString()))
        {
            default:
            case "#EDF50C":
                color_button01.GetComponent<Button>().interactable = false;
                break;
            case "#17A4EB":
                color_button02.GetComponent<Button>().interactable = false;
                break;
            case "#4BC96E":
                color_button03.GetComponent<Button>().interactable = false;
                break;

        }
       
        current_error = (error_Highlighting)PlayerPrefs.GetInt(pref_keys.error_highlighting_int.ToString());
        error_button.GetComponentInChildren<Text>().text = current_error.ToString();
        color_button01.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Yellow); });
        color_button02.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Blue); });
        color_button03.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Green); });
        color_buttons.AddRange(new List<Button> {
            color_button01.GetComponent<Button>(),
            color_button02.GetComponent<Button>(),
            color_button03.GetComponent<Button>()
        });
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
        PlayerPrefs.SetInt(pref_keys.error_highlighting_int.ToString(), j);
        error_button.GetComponentInChildren<Text>().text = current_error.ToString();
    }

    private void ColorButtons(color_Themes color)
    {
        string col = "";
        foreach (Button button in color_buttons)
        {
            button.interactable = true;
        }
        switch (color)
        {
            default:
            case color_Themes.Yellow:
                col = "#EDF50C";
                color_button01.GetComponent<Button>().interactable = false;
                break;
            case color_Themes.Blue:
                col = "#17A4EB";
                color_button02.GetComponent<Button>().interactable = false;
                break;
            case color_Themes.Green:
                col= "#4BC96E";
                color_button03.GetComponent<Button>().interactable = false;
                break;

        }
        PlayerPrefs.SetString(pref_keys.c_tile_highlighted_str.ToString(), col);
        PlayerPrefs.SetString(pref_keys.c_error_str.ToString(), "#FF0000");
        PlayerPrefs.SetString(pref_keys.c_tile_default_str.ToString(), "#f2f2f2");
    }
}
